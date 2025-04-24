using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.Expense.Delete
{
	public class DeleteExpenseCommandHandler : IRequestHandler<DeleteExpenseCommandRequest, DeleteExpenseCommandResponse>
	{
		private readonly IExpenseService _expenseService;

		public DeleteExpenseCommandHandler(IExpenseService expenseService)
		{
			_expenseService = expenseService;
		}

		public async Task<DeleteExpenseCommandResponse> Handle(DeleteExpenseCommandRequest request, CancellationToken cancellationToken)
		{
			var expense = await _expenseService.GetByIdAsync(request.Id);
			await _expenseService.DeleteAsync(request.Id);
			return new DeleteExpenseCommandResponse
			{
				Success = true,
				Message = "Expense deleted successfully"
			};
		}
	}
}
