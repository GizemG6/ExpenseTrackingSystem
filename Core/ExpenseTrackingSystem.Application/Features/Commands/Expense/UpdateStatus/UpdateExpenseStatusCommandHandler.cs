using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.Expense.UpdateStatus
{
	public class UpdateExpenseStatusCommandHandler : IRequestHandler<UpdateExpenseStatusCommandRequest, UpdateExpenseStatusCommandResponse>
	{
		private readonly IExpenseService _expenseService;

		public UpdateExpenseStatusCommandHandler(IExpenseService expenseService)
		{
			_expenseService = expenseService;
		}

		public async Task<UpdateExpenseStatusCommandResponse> Handle(UpdateExpenseStatusCommandRequest request, CancellationToken cancellationToken)
		{
			var expense = await _expenseService.GetByIdAsync(request.Id);
			expense.Status = request.Status;
			expense.RejectionReason = request.RejectionReason;
			await _expenseService.UpdateStatusAsync(expense);
			return new UpdateExpenseStatusCommandResponse
			{
				Success = true,
				Message = request.Status == ExpenseStatus.Approved ? "Expense approved successfully." : "Expense rejected successfully."
			};
		}
	}
}
