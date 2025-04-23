using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.ExpenseCategory.Update
{
	public class UpdateExpenseCategoryCommandHandler : IRequestHandler<UpdateExpenseCategoryCommandRequest, UpdateExpenseCategoryCommandResponse>
	{
		private readonly IExpenseCategoryService _expenseCategoryService;

		public UpdateExpenseCategoryCommandHandler(IExpenseCategoryService expenseCategoryService)
		{
			_expenseCategoryService = expenseCategoryService;
		}

		public async Task<UpdateExpenseCategoryCommandResponse> Handle(UpdateExpenseCategoryCommandRequest request, CancellationToken cancellationToken)
		{
			var category = await _expenseCategoryService.GetByIdAsync(request.Id);
			category.Name = request.Name;
			await _expenseCategoryService.UpdateAsync(category.Id, category.Name);
			return new UpdateExpenseCategoryCommandResponse
			{
				Success = true,
				Message = "Expense category updated successfully."
			};
		}
	}
}
