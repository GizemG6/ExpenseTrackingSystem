using ExpenseTrackingSystem.Application.Features.Commands.ExpenseCategory.Create;
using ExpenseTrackingSystem.Application.Features.Commands.ExpenseCategory.Delete;
using ExpenseTrackingSystem.Application.Features.Commands.ExpenseCategory.Update;
using ExpenseTrackingSystem.Application.Features.Queries.ExpenseCategory.GetAll;
using ExpenseTrackingSystem.Application.Features.Queries.ExpenseCategory.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpenseCategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

		public ExpenseCategoriesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllExpenseCategories()
		{
			var response = await _mediator.Send(new GetAllExpenseCategoriesQueryRequest());
			return Ok(response);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetExpenseCategoryById([FromRoute]GetExpenseCategoryByIdQueryRequest getExpenseCategoryByIdQueryRequest)
		{
			GetExpenseCategoryByIdQueryResponse response = await _mediator.Send(getExpenseCategoryByIdQueryRequest);
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateExpenseCategory(CreateExpenseCategoryCommandRequest createExpenseCategoryCommandRequest)
		{
			CreateExpenseCategoryCommandResponse response = await _mediator.Send(createExpenseCategoryCommandRequest);
			return Ok(response);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateExpenseCategory(UpdateExpenseCategoryCommandRequest updateExpenseCategoryCommandRequest)
		{
			UpdateExpenseCategoryCommandResponse response = await _mediator.Send(updateExpenseCategoryCommandRequest);
			return Ok(response);
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteExpenseCategory(DeleteExpenseCategoryCommandRequest deleteExpenseCategoryCommandRequest)
		{
			DeleteExpenseCategoryCommandResponse response = await _mediator.Send(deleteExpenseCategoryCommandRequest);
			return Ok(response);
		}
	}
}
