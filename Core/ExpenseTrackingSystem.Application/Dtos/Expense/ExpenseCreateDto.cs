using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Dtos.Expense
{
	public class ExpenseCreateDto
	{
		public long UserId { get; set; }
		public int CategoryId { get; set; }
		public decimal Amount { get; set; }
		public string? Description { get; set; }
		public DateTime Date { get; set; }
		public string Location { get; set; }
		public IFormFile? ReceiptFile { get; set; }
	}
}
