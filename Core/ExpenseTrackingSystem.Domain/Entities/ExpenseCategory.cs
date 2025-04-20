using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Domain.Entities
{
	public class ExpenseCategory
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public ICollection<Expense> Expenses { get; set; }
	}
}
