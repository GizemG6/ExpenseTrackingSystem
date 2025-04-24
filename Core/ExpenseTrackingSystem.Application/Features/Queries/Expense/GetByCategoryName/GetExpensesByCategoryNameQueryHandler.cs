using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.Expense.GetByCategoryName
{
	public class GetExpensesByCategoryNameQueryHandler : IRequestHandler<GetExpensesByCategoryNameQueryRequest, List<GetExpensesByCategoryNameQueryResponse>>
	{
		private readonly IExpenseService _expenseService;

		public GetExpensesByCategoryNameQueryHandler(IExpenseService expenseService)
		{
			_expenseService = expenseService;
		}

		public async Task<List<GetExpensesByCategoryNameQueryResponse>> Handle(GetExpensesByCategoryNameQueryRequest request, CancellationToken cancellationToken)
		{
			var expenses = await _expenseService.GetByCategoryAsync(request.CategoryName);
			return expenses.Select(e => new GetExpensesByCategoryNameQueryResponse
			{
				Id = e.Id,
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
