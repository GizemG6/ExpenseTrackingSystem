using ExpenseTrackingSystem.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Dtos.Expense
{
	public class ExpenseCreateDto
	{
		public string UserId { get; set; }
		public int CategoryId { get; set; }
		public decimal Amount { get; set; }
		public DateTime Date { get; set; }
		public string Location { get; set; }
		public ExpenseStatus Status { get; set; }
		public IFormFile? ReceiptFile { get; set; }
	}
}
