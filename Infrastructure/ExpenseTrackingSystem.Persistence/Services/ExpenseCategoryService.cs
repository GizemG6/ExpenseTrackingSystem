using AutoMapper;
using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Repositories;
using ExpenseTrackingSystem.Domain.Entities;
using ExpenseTrackingSystem.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Persistence.Services
{
	public class ExpenseCategoryService : IExpenseCategoryService
	{
		private readonly IExpenseCategoryReadRepository _expenseCategoryReadRepository;
		private readonly IExpenseCategoryWriteRepository _expenseCategoryWriteRepository;
		private readonly IMapper _mapper;

		public ExpenseCategoryService(IExpenseCategoryReadRepository expenseCategoryReadRepository, 
			IExpenseCategoryWriteRepository expenseCategoryWriteRepository, IMapper mapper)
		{
			_expenseCategoryReadRepository = expenseCategoryReadRepository;
			_expenseCategoryWriteRepository = expenseCategoryWriteRepository;
			_mapper = mapper;
		}

		public async Task<bool> CreateAsync(string name)
		{
			var category = new ExpenseCategory { Name = name };
			await _expenseCategoryWriteRepository.AddAsync(category);
			await _expenseCategoryWriteRepository.SaveChangesAsync();
			return true;
		}

		public async Task<bool> DeleteAsync(int id)
		{
			return await _expenseCategoryWriteRepository.RemoveAsync(id);
		}

		public async Task<List<ExpenseCategory>> GetAllAsync()
		{
			return await _expenseCategoryReadRepository.GetAllAsync();
		}

		public async Task<ExpenseCategory?> GetByIdAsync(int id)
		{
			return await _expenseCategoryReadRepository.GetByIdAsync(id);
		}

		public async Task<bool> UpdateAsync(int id, string name)
		{
			var category = await _expenseCategoryReadRepository.GetByIdAsync(id);
			if (category == null) return false;

			category.Name = name;
			await _expenseCategoryWriteRepository.UpdateAsync(category);
			await _expenseCategoryWriteRepository.SaveChangesAsync();
			return true;
		}
	}
}
