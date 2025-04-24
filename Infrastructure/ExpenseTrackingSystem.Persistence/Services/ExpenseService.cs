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

		public Task<bool> DeleteAsync(Guid id)
		{
			throw new NotImplementedException();
		}

		public async Task<List<Expense>> GetAllAsync()
		{
			return await _expenseReadRepository.GetAllAsync();
		}

		public async Task<Expense> GetByIdAsync(Guid id)
		{
			var expense = await _expenseReadRepository.GetByIdAsync(id);
			if (expense == null)
				throw new Exception("Expense not found");

			return expense;
		}

		public Task<List<Expense>> GetByStatusAsync(ExpenseStatus status)
		{
			throw new NotImplementedException();
		}

		public Task<List<Expense>> GetByUserIdAsync(string userId)
		{
			throw new NotImplementedException();
		}

		public Task<Expense> UpdateAsync(Expense expense)
		{
			throw new NotImplementedException();
		}
	}
}
