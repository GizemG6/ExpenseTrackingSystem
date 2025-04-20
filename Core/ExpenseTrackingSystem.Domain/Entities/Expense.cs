using ExpenseTrackingSystem.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Domain.Entities
{
	public class Expense
	{
		public Guid Id { get; set; }
		public string UserId { get; set; }
		public int CategoryId { get; set; }
		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
		public string Location { get; set; }
		public ExpenseStatus Status { get; set; }
		public string? RejectionReason { get; set; }
		public string? ReceiptFilePath { get; set; }

		public ExpenseCategory Category { get; set; }
		public AppUser User { get; set; }
	}

	public enum ExpenseStatus
	{
		Pending = 1,
		Approved,
		Rejected
	}
}
