using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.ExpenseCategory.GetById
{
	public class GetExpenseCategoryByIdQueryHandler : IRequestHandler<GetExpenseCategoryByIdQueryRequest, GetExpenseCategoryByIdQueryResponse>
	{
		private readonly IExpenseCategoryService _expenseCategoryService;

		public GetExpenseCategoryByIdQueryHandler(IExpenseCategoryService expenseCategoryService)
		{
			_expenseCategoryService = expenseCategoryService;
		}

		public async Task<GetExpenseCategoryByIdQueryResponse> Handle(GetExpenseCategoryByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var category = await _expenseCategoryService.GetByIdAsync(request.Id);
			return new()
			{
				Name = category.Name
			};
		}
	}
}
