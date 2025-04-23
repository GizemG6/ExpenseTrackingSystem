using ExpenseTrackingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Abstractions.Services
{
	public interface IExpenseCategoryService
	{
		Task<bool> CreateAsync(string name);
		Task<bool> UpdateAsync(int id, string name);
		Task<bool> DeleteAsync(int id);
		Task<ExpenseCategory?> GetByIdAsync(int id);
		Task<List<ExpenseCategory>> GetAllAsync();
	}
}
