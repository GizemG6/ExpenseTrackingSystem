using ExpenseTrackingSystem.Application.Features.Commands.ExpenseCategory.Create;
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

		[HttpPost]
		public async Task<IActionResult> CreateExpenseCategory(CreateExpenseCategoryCommandRequest createExpenseCategoryCommandRequest)
		{
			CreateExpenseCategoryCommandResponse response = await _mediator.Send(createExpenseCategoryCommandRequest);
			return Ok(response);
		}
	}
}
