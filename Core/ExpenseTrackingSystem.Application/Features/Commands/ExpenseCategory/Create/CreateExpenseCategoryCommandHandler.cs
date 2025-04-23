using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.ExpenseCategory.Create
{
	public class CreateExpenseCategoryCommandHandler : IRequestHandler<CreateExpenseCategoryCommandRequest, CreateExpenseCategoryCommandResponse>
	{
		private readonly IExpenseCategoryService _expenseCategoryService;

		public CreateExpenseCategoryCommandHandler(IExpenseCategoryService expenseCategoryService)
		{
			_expenseCategoryService = expenseCategoryService;
		}

		public async Task<CreateExpenseCategoryCommandResponse> Handle(CreateExpenseCategoryCommandRequest request, CancellationToken cancellationToken)
		{
			var category = await _expenseCategoryService.CreateAsync(request.Name);
			if (category)
			{
				return new CreateExpenseCategoryCommandResponse
				{
					Success = true,
					Message = "Expense category created successfully."
				};
			}
			else
			{
				return new CreateExpenseCategoryCommandResponse
				{
					Success = false,
					Message = "Failed to create expense category."
				};
			}
		}
	}
}
