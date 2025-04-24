using ExpenseTrackingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.Expense.GetByCategoryName
{
	public class GetExpensesByCategoryNameQueryResponse
	{
		public Guid Id { get; set; }
		public string UserId { get; set; }
		public int CategoryId { get; set; }
		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
		public string Location { get; set; }
		public ExpenseStatus Status { get; set; }
		public string? ReceiptFilePath { get; set; }
	}
}
