using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.ExpenseCategory.GetAll
{
	public class GetAllExpenseCategoriesQueryHandler : IRequestHandler<GetAllExpenseCategoriesQueryRequest, List<GetAllExpenseCategoriesQueryResponse>>
	{
		private readonly IExpenseCategoryService _expenseCategoryService;

		public GetAllExpenseCategoriesQueryHandler(IExpenseCategoryService expenseCategoryService)
		{
			_expenseCategoryService = expenseCategoryService;
		}

		public async Task<List<GetAllExpenseCategoriesQueryResponse>> Handle(GetAllExpenseCategoriesQueryRequest request, CancellationToken cancellationToken)
		{
			var categories = await _expenseCategoryService.GetAllAsync();
			var response = categories.Select(c => new GetAllExpenseCategoriesQueryResponse
			{
				Name = c.Name
			}).ToList();
			return response;
		}
	}
}
