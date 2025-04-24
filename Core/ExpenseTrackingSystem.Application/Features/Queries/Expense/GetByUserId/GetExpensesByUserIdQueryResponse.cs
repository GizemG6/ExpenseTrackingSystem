using ExpenseTrackingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.Expense.GetByUserId
{
	public class GetExpensesByUserIdQueryResponse
	{
		public string UserId { get; set; }
		public int CategoryId { get; set; }
		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
		public string Location { get; set; }
		public ExpenseStatus Status { get; set; }
		public string? ReceiptFilePath { get; set; }
	}
}
