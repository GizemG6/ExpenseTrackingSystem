using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.Expense.GetByUserId
{
	public class GetExpensesByUserIdQueryHandler : IRequestHandler<GetExpensesByUserIdQueryRequest, List<GetExpensesByUserIdQueryResponse>>
	{
		private readonly IExpenseService _expenseService;

		public GetExpensesByUserIdQueryHandler(IExpenseService expenseService)
		{
			_expenseService = expenseService;
		}

		public async Task<List<GetExpensesByUserIdQueryResponse>> Handle(GetExpensesByUserIdQueryRequest request, CancellationToken cancellationToken)
		{
			var expenses = await _expenseService.GetByUserIdAsync(request.UserId);
			return expenses
				.Select(e => new GetExpensesByUserIdQueryResponse
				{
					UserId = e.UserId,
					CategoryId = e.CategoryId,
					Amount = e.Amount,
					Date = e.Date,
					Location = e.Location,
					Status = e.Status,
					ReceiptFilePath = e.ReceiptFilePath
				}).ToList();
		}
	}
}
