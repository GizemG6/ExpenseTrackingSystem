using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.Expense.GetAll
{
	public class GetAllExpensesQueryHandler : IRequestHandler<GetAllExpensesQueryRequest, List<GetAllExpensesQueryResponse>>
	{
		private readonly IExpenseService _expenseService;

		public GetAllExpensesQueryHandler(IExpenseService expenseService)
		{
			_expenseService = expenseService;
		}

		public async Task<List<GetAllExpensesQueryResponse>> Handle(GetAllExpensesQueryRequest request, CancellationToken cancellationToken)
		{
			var expenses = await _expenseService.GetAllAsync();
			return expenses.Select(e => new GetAllExpensesQueryResponse
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
