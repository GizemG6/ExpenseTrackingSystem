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
	public class ExpenseReadRepository : ReadRepository<Expense, Guid>, IExpenseReadRepository
	{
		public ExpenseReadRepository(AppDbContext context) : base(context) { }
	}
}
