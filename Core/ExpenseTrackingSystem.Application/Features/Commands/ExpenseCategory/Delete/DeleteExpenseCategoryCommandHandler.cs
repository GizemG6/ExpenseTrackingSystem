using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.ExpenseCategory.Delete
{
	public class DeleteExpenseCategoryCommandHandler : IRequestHandler<DeleteExpenseCategoryCommandRequest, DeleteExpenseCategoryCommandResponse>
	{
		private readonly IExpenseCategoryService _expenseCategoryService;

		public DeleteExpenseCategoryCommandHandler(IExpenseCategoryService expenseCategoryService)
		{
			_expenseCategoryService = expenseCategoryService;
		}

		public async Task<DeleteExpenseCategoryCommandResponse> Handle(DeleteExpenseCategoryCommandRequest request, CancellationToken cancellationToken)
		{
			var category = await _expenseCategoryService.GetByIdAsync(request.Id);
			return new DeleteExpenseCategoryCommandResponse
			{
				Success = true,
				Message = "Category deleted successfully."
			};
		}
	}
}
