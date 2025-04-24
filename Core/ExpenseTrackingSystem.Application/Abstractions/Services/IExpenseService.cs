using ExpenseTrackingSystem.Application.Dtos.Expense;
using ExpenseTrackingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Abstractions.Services
{
	public interface IExpenseService
	{
		Task<List<Expense>> GetAllAsync();
		Task<Expense> GetByIdAsync(Guid id);
		Task<Expense> CreateAsync(ExpenseCreateDto expenseCreateDto);
		Task<Expense> UpdateAsync(Expense expense);
		Task<bool> DeleteAsync(Guid id);
		Task<List<Expense>> GetByStatusAsync(ExpenseStatus status);
		Task<List<Expense>> GetByUserIdAsync(string userId);
		Task<List<Expense>> GetByFullNameAsync(string fullName);
	}
}
