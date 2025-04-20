using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Dtos.Expense
{
	public class ExpenseDto
	{
		public long Id { get; set; }
		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
		public string Location { get; set; }
		public string Status { get; set; }
		public string? ReceiptFilePath { get; set; }
		public string CategoryName { get; set; }
		public string UserFullName { get; set; }
	}
}
