using ExpenseTrackingSystem.Application.Features.Commands.User.LoginUser;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackingSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthsController : ControllerBase
    {
		readonly IMediator _mediator;

		public AuthsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
		{
			LoginUserCommandResponse response = await _mediator.Send(loginUserCommandRequest);
			return Ok(response);
		}
	}
}
