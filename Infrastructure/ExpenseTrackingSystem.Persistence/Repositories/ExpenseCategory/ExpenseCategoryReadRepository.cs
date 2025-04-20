using ExpenseTrackingSystem.Application.Repositories;
using ExpenseTrackingSystem.Domain.Entities;
using ExpenseTrackingSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Persistence.Repositories
{
	public class ExpenseCategoryReadRepository : ReadRepository<ExpenseCategory, int>, IExpenseCategoryReadRepository
	{
		public ExpenseCategoryReadRepository(AppDbContext context) : base(context) { }
	}
}
