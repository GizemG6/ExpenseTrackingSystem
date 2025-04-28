using ExpenseTrackingSystem.Application.Features.Commands.Expense.Create;
using ExpenseTrackingSystem.Application.Features.Commands.Expense.Delete;
using ExpenseTrackingSystem.Application.Features.Commands.Expense.UpdateStatus;
using ExpenseTrackingSystem.Application.Features.Queries.Expense.GetAll;
using ExpenseTrackingSystem.Application.Features.Queries.Expense.GetByCategoryName;
using ExpenseTrackingSystem.Application.Features.Queries.Expense.GetByFullName;
using ExpenseTrackingSystem.Application.Features.Queries.Expense.GetById;
using ExpenseTrackingSystem.Application.Features.Queries.Expense.GetByStatus;
using ExpenseTrackingSystem.Application.Features.Queries.Expense.GetByUserId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
		[Authorize(Roles = "Admin")]
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

		[HttpGet("by-status")]
		public async Task<IActionResult> GetExpensesByStatus([FromQuery]GetExpensesByStatusQueryRequest getExpensesByStatusQueryRequest)
		{
			List<GetExpensesByStatusQueryResponse> response = await _mediator.Send(getExpensesByStatusQueryRequest);
			return Ok(response);
		}

		[HttpGet("by-userId")]
		public async Task<IActionResult> GetExpensesByUserId([FromQuery]GetExpensesByUserIdQueryRequest getExpensesByUserIdQueryRequest)
		{
			List<GetExpensesByUserIdQueryResponse> response = await _mediator.Send(getExpensesByUserIdQueryRequest);
			return Ok(response);
		}

		[HttpGet("by-full-name")]
		public async Task<IActionResult> GetExpensesByFullName([FromQuery]GetExpensesByFullNameQueryRequest getExpensesByFullNameQueryRequest)
		{
			List<GetExpensesByFullNameQueryResponse> response = await _mediator.Send(getExpensesByFullNameQueryRequest);
			return Ok(response);
		}

		[HttpGet("by-category-name")]
		public async Task<IActionResult> GetExpensesByCategoryName([FromQuery]GetExpensesByCategoryNameQueryRequest getExpensesByCategoryNameQueryRequest)
		{
			List<GetExpensesByCategoryNameQueryResponse> response = await _mediator.Send(getExpensesByCategoryNameQueryRequest);
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateExpense([FromForm]CreateExpenseCommandRequest createExpenseCommandRequest)
		{
			CreateExpenseCommandResponse response = await _mediator.Send(createExpenseCommandRequest);
			return Ok(response);
		}

		[HttpPut]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> UpdateExpenseStatus([FromBody]UpdateExpenseStatusCommandRequest updateExpenseStatusCommandRequest)
		{
			UpdateExpenseStatusCommandResponse response = await _mediator.Send(updateExpenseStatusCommandRequest);
			return Ok(response);
		}

		[HttpDelete("Id")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteExpense([FromBody]DeleteExpenseCommandRequest deleteExpenseCommandRequest)
		{
			DeleteExpenseCommandResponse response = await _mediator.Send(deleteExpenseCommandRequest);
			return Ok(response);
		}
	}
}
