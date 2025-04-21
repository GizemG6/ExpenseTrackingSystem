using ExpenseTrackingSystem.Application.Features.Commands.User.CreateUser;
using ExpenseTrackingSystem.Application.Features.Queries.User.GetAllUsers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackingSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IMediator _mediator;

		public UsersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var response = await _mediator.Send(new GetAllUsersQueryRequest());
			return Ok(response);
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
		{
			CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
			return Ok(response);
		}
	}
}
