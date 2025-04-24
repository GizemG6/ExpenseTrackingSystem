using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.Expense.Delete
{
	public class DeleteExpenseCommandRequest : IRequest<DeleteExpenseCommandResponse>
	{
		public Guid Id { get; set; }
	}
}
