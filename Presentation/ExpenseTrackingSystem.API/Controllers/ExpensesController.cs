using ExpenseTrackingSystem.Application.Features.Commands.Expense.Create;
using ExpenseTrackingSystem.Application.Features.Queries.Expense.GetAll;
using ExpenseTrackingSystem.Application.Features.Queries.Expense.GetById;
using ExpenseTrackingSystem.Application.Features.Queries.ExpenseCategory.GetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackingSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ExpensesController : ControllerBase
	{
		private readonly IMediator _mediator;

		public ExpensesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllExpenses()
		{
			var response = await _mediator.Send(new GetAllExpensesQueryRequest());
			return Ok(response);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetExpenseById([FromRoute]GetExpenseByIdQueryRequest getExpenseByIdQueryRequest)
		{
			GetExpenseByIdQueryResponse response = await _mediator.Send(getExpenseByIdQueryRequest);
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateExpense([FromForm]CreateExpenseCommandRequest createExpenseCommandRequest)
		{
			CreateExpenseCommandResponse response = await _mediator.Send(createExpenseCommandRequest);
			return Ok(response);
		}
	}
}
