using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.Expense.GetById
{
	public class GetExpenseByIdQueryHandler : IRequestHandler<GetExpenseByIdQueryRequest, GetExpenseByIdQueryResponse>
	{
		private readonly IExpenseService _expenseService;

		public GetExpenseByIdQueryHandler(IExpenseService expenseService)
		{
			_expenseService = expenseService;
		}

		public async Task<GetExpenseByIdQueryResponse> Handle(GetExpenseByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var expense = await _expenseService.GetByIdAsync(request.Id);
			return new GetExpenseByIdQueryResponse
			{
				Id = expense.Id,
				UserId = expense.UserId,
				CategoryId = expense.CategoryId,
				Amount = expense.Amount,
				Date = expense.Date,
				Location = expense.Location,
				Status = expense.Status,
				ReceiptFilePath = expense.ReceiptFilePath
			};
		}
	}
}
