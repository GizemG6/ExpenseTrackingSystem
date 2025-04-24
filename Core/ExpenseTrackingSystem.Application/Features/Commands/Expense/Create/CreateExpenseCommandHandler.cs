using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Dtos.Expense;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.Expense.Create
{
	public class CreateExpenseCommandHandler : IRequestHandler<CreateExpenseCommandRequest, CreateExpenseCommandResponse>
	{
		private readonly IExpenseService _expenseService;

		public CreateExpenseCommandHandler(IExpenseService expenseService)
		{
			_expenseService = expenseService;
		}

		public async Task<CreateExpenseCommandResponse> Handle(CreateExpenseCommandRequest request, CancellationToken cancellationToken)
		{
			var expense = new ExpenseCreateDto
			{
				Amount = request.Amount,
				CategoryId = request.CategoryId,
				Date = request.Date,
				Location = request.Location,
				UserId = request.UserId,
				ReceiptFile = request.ReceiptFile
			};

			var result = await _expenseService.CreateAsync(expense);

			return new CreateExpenseCommandResponse
			{
				Success = result != null,
				Message = result != null ? "Expense created successfully." : "Failed to create expense."
			};
		}
	}
}
