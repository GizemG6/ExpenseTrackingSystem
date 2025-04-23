using ExpenseTrackingSystem.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.Expense.Create
{
	public class CreateExpenseCommandRequest : IRequest<CreateExpenseCommandResponse>
	{
		public string UserId { get; set; }
		public int CategoryId { get; set; }
		public decimal Amount { get; set; }
		public string? Description { get; set; }
		public DateTime Date { get; set; }
		public string Location { get; set; }
		public ExpenseStatus Status { get; set; }
		public IFormFile? ReceiptFile { get; set; }
	}
}
