using ExpenseTrackingSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.Expense.UpdateStatus
{
	public class UpdateExpenseStatusCommandRequest : IRequest<UpdateExpenseStatusCommandResponse>
	{
		public Guid Id { get; set; }
		public ExpenseStatus Status { get; set; }
		public string? RejectionReason { get; set; }
	}
}
