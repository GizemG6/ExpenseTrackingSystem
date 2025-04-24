using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Dtos.Expense;
using ExpenseTrackingSystem.Application.Helpers;
using ExpenseTrackingSystem.Application.Repositories;
using ExpenseTrackingSystem.Domain.Entities;
using ExpenseTrackingSystem.Domain.Entities.Identity;
using ExpenseTrackingSystem.Persistence.Repositories;
using FluentValidation;
using Humanizer;
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
		private readonly UserManager<AppUser> _userManager;	

		public ExpenseService(IExpenseReadRepository expenseReadRepository, IExpenseWriteRepository expenseWriteRepository, 
			UserManager<AppUser> userManager, IExpenseCategoryReadRepository expenseCategoryReadRepository)
		{
			_expenseReadRepository = expenseReadRepository;
			_expenseWriteRepository = expenseWriteRepository;
			_userManager = userManager;
			_expenseCategoryReadRepository = expenseCategoryReadRepository;
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
			var existingExpense = await _expenseReadRepository.GetByIdAsync(expense.Id);
			if (existingExpense == null)
				throw new Exception("Expense not found");

			if (expense.Status == ExpenseStatus.Rejected && string.IsNullOrWhiteSpace(expense.RejectionReason))
			{
				throw new Exception("Rejection reason is required when status is Rejected.");
			}

			existingExpense.Status = expense.Status;
			existingExpense.RejectionReason = expense.Status == ExpenseStatus.Rejected
				? expense.RejectionReason
				: null;

			await _expenseWriteRepository.UpdateAsync(existingExpense);
			await _expenseWriteRepository.SaveChangesAsync();

			return existingExpense;
		}
	}
}
