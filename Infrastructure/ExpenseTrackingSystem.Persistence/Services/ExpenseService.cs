using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Dtos.Expense;
using ExpenseTrackingSystem.Application.Helpers;
using ExpenseTrackingSystem.Application.Repositories;
using ExpenseTrackingSystem.Application.Repositories.Payment;
using ExpenseTrackingSystem.Domain.Entities;
using ExpenseTrackingSystem.Domain.Entities.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Persistence.Services
{
	public class ExpenseService : IExpenseService
	{
		private readonly IExpenseReadRepository _expenseReadRepository;
		private readonly IExpenseWriteRepository _expenseWriteRepository;
		private readonly IExpenseCategoryReadRepository _expenseCategoryReadRepository;
		private readonly IPaymentReadRepository _paymentReadRepository;
		private readonly IPaymentWriteRepository _paymentWriteRepository;
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<AppRole> _roleManager;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IAuditLogService _auditLogService;
		private readonly IMailService _mailService;

		public ExpenseService(IExpenseReadRepository expenseReadRepository, IExpenseWriteRepository expenseWriteRepository,
			UserManager<AppUser> userManager, IExpenseCategoryReadRepository expenseCategoryReadRepository, 
			IPaymentReadRepository paymentReadRepository, IPaymentWriteRepository paymentWriteRepository,
			IHttpContextAccessor httpContextAccessor, IAuditLogService auditLogService, IMailService mailService, RoleManager<AppRole> roleManager)
		{
			_expenseReadRepository = expenseReadRepository;
			_expenseWriteRepository = expenseWriteRepository;
			_userManager = userManager;
			_expenseCategoryReadRepository = expenseCategoryReadRepository;
			_paymentReadRepository = paymentReadRepository;
			_paymentWriteRepository = paymentWriteRepository;
			_httpContextAccessor = httpContextAccessor;
			_auditLogService = auditLogService;
			_mailService = mailService;
			_roleManager = roleManager;
		}

		public async Task<Expense> CreateAsync(ExpenseCreateDto expenseCreateDto)
		{
			var user = await _userManager.FindByIdAsync(expenseCreateDto.UserId);
			if (user == null)
				throw new Exception("User not found");

			var category = await _expenseCategoryReadRepository.GetByIdAsync(expenseCreateDto.CategoryId);
			if (category == null)
				throw new Exception("Category not found");

			string? receiptPath = await FileHelper.SaveReceiptFileAsync(expenseCreateDto.ReceiptFile);

			var expense = new Expense
			{
				Id = Guid.NewGuid(),
				UserId = expenseCreateDto.UserId,
				CategoryId = expenseCreateDto.CategoryId,
				Amount = expenseCreateDto.Amount,
				Date = expenseCreateDto.Date,
				Location = expenseCreateDto.Location,
				Status = ExpenseStatus.Pending,
				ReceiptFilePath = receiptPath
			};

			await _expenseWriteRepository.AddAsync(expense);
			await _expenseWriteRepository.SaveChangesAsync();
			await _auditLogService.LogActionAsync(expense.UserId, "Create", "Expense", expense.Id.ToString());

			await SendExpenseCreatedMailAsync(user, category, expense);

			return expense;
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			return await _expenseWriteRepository.RemoveAsync(id);
		}

		public async Task<List<Expense>> GetAllAsync()
		{
			return await _expenseReadRepository.GetAllAsync();
		}

		public async Task<List<Expense>> GetByCategoryAsync(string categoryName)
		{
			var category = await _expenseCategoryReadRepository
				.GetWhere(c => c.Name.ToLower() == categoryName.ToLower())
				.FirstOrDefaultAsync();

			if (category == null)
				throw new Exception("Category not found");

			var expenses = _expenseReadRepository
				.GetWhere(e => e.CategoryId == category.Id);

			return await expenses.ToListAsync();
		}

		public async Task<List<Expense>> GetByFullNameAsync(string fullName)
		{
			var user = await _userManager.Users.FirstOrDefaultAsync(u => u.FullName == fullName);
			if (user == null)
				throw new Exception("User not found");

			var expenses = _expenseReadRepository.GetWhere(e => e.UserId == user.Id);
			return await expenses.ToListAsync();
		}

		public async Task<Expense> GetByIdAsync(Guid id)
		{
			var expense = await _expenseReadRepository.GetByIdAsync(id);
			if (expense == null)
				throw new Exception("Expense not found");

			return expense;
		}

		public async Task<List<Expense>> GetByStatusAsync(ExpenseStatus status)
		{
			var expenses = _expenseReadRepository.GetWhere(e => e.Status == status);
			return await expenses.ToListAsync();
		}

		public async Task<List<Expense>> GetByUserIdAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user == null)
				throw new Exception("User not found");
			var expenses = _expenseReadRepository.GetWhere(e => e.UserId == userId);
			return await expenses.ToListAsync();
		}

		public async Task<Expense> UpdateStatusAsync(Expense expense)
		{
			var existingExpense = await _expenseReadRepository.GetByIdAsync(expense.Id)
				?? throw new Exception("Expense not found");

			ValidateRejectionReason(expense);
			await UpdateExpenseStatusAsync(existingExpense, expense);

			if (expense.Status == ExpenseStatus.Approved)
				await CreatePaymentSimulationAsync(existingExpense);

			var user = await _userManager.FindByIdAsync(existingExpense.UserId);
			if (user != null)
			{
				string expenseStatus = expense.Status.ToString();
				await _mailService.SendExpenseStatusUpdateMailAsync(user.Email, expenseStatus, expense.Id.ToString());
			}

			await _auditLogService.LogActionAsync(expense.UserId, "UpdateStatus", "Expense", expense.Id.ToString());

			return existingExpense;
		}

		private void ValidateRejectionReason(Expense expense)
		{
			if (expense.Status == ExpenseStatus.Rejected && string.IsNullOrWhiteSpace(expense.RejectionReason))
				throw new Exception("Rejection reason is required when status is Rejected.");
		}

		private async Task UpdateExpenseStatusAsync(Expense existingExpense, Expense updatedExpense)
		{
			existingExpense.Status = updatedExpense.Status;
			existingExpense.RejectionReason = updatedExpense.Status == ExpenseStatus.Rejected
				? updatedExpense.RejectionReason
				: null;

			await _expenseWriteRepository.UpdateAsync(existingExpense);
			await _expenseWriteRepository.SaveChangesAsync();
		}

		private async Task CreatePaymentSimulationAsync(Expense expense)
		{
			var receiver = await _userManager.FindByIdAsync(expense.UserId)
				?? throw new Exception("Receiver (AppUser) not found for the expense.");

			var currentUser = _httpContextAccessor.HttpContext?.User;

			var sender = await _userManager.GetUserAsync(currentUser)
				?? throw new Exception("Current admin user not found.");

			var payment = new PaymentSimulation
			{
				Id = Guid.NewGuid(),
				PaymentDate = DateTime.UtcNow,
				BankReferenceNo = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
				Expense = expense,
				PaidAmount = expense.Amount,
				SenderFullName = sender.FullName,
				SenderIban = sender.IBAN,
				ReceiverFullName = receiver.FullName,
				ReceiverIban = receiver.IBAN
			};

			await _paymentWriteRepository.AddAsync(payment);
			await _paymentWriteRepository.SaveChangesAsync();
		}

		private async Task<IEnumerable<string>> GetAdminEmailsAsync()
		{
			var adminRole = await _roleManager.FindByNameAsync("Admin");

			if (adminRole == null)
			{
				return Enumerable.Empty<string>();
			}

			var users = await _userManager.Users.ToListAsync();

			var adminEmails = new List<string>();

			foreach (var user in users)
			{
				if (await _userManager.IsInRoleAsync(user, adminRole.Name))
				{
					adminEmails.Add(user.Email);
				}
			}

			return adminEmails;
		}

		private async Task SendExpenseCreatedMailAsync(AppUser user, ExpenseCategory category, Expense expense)
		{
			var adminEmails = await GetAdminEmailsAsync();

			await _mailService.SendExpenseCreatedMailAsync(
				adminEmails.ToArray(),
				user.UserName,
				category.Name,
				expense.Amount,
				expense.Date,
				expense.Id.ToString()
			);
		}
	}
}
