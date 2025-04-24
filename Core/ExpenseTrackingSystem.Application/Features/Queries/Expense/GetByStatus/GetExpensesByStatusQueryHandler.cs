using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.Expense.GetByStatus
{
	public class GetExpensesByStatusQueryHandler : IRequestHandler<GetExpensesByStatusQueryRequest, List<GetExpensesByStatusQueryResponse>>
	{
		private readonly IExpenseService _expenseService;

		public GetExpensesByStatusQueryHandler(IExpenseService expenseService)
		{
			_expenseService = expenseService;
		}

		public async Task<List<GetExpensesByStatusQueryResponse>> Handle(GetExpensesByStatusQueryRequest request, CancellationToken cancellationToken)
		{
			var expenses = await _expenseService.GetByStatusAsync(request.Status);
			return expenses.Select(e => new GetExpensesByStatusQueryResponse
			{
				UserId = e.UserId,
				CategoryId = e.CategoryId,
				Amount = e.Amount,
				Date = e.Date,
				Location = e.Location,
				ReceiptFilePath = e.ReceiptFilePath
			}).ToList();
		}
	}
}
