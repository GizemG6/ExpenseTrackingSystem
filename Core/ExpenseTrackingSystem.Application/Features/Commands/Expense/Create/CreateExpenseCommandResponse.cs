using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.Expense.Create
{
	public class CreateExpenseCommandResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }
	}
}
