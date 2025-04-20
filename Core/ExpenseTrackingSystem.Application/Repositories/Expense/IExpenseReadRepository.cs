using ExpenseTrackingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Repositories
{
	public interface IExpenseReadRepository : IReadRepository<Expense, Guid>
	{
	}
}
