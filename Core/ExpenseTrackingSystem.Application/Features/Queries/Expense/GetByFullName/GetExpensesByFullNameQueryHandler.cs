using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.Expense.GetByFullName
{
	public class GetExpensesByFullNameQueryHandler : IRequestHandler<GetExpensesByFullNameQueryRequest, List<GetExpensesByFullNameQueryResponse>>
	{
		private readonly IExpenseService _expenseService;

		public GetExpensesByFullNameQueryHandler(IExpenseService expenseService)
		{
			_expenseService = expenseService;
		}

		public async Task<List<GetExpensesByFullNameQueryResponse>> Handle(GetExpensesByFullNameQueryRequest request, CancellationToken cancellationToken)
		{
			var expenses = await _expenseService.GetByFullNameAsync(request.FullName);
			return expenses.Select(e => new GetExpensesByFullNameQueryResponse
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
