using ExpenseTrackingSystem.Application.Features.Commands.User.LoginUser;
using ExpenseTrackingSystem.Application.Features.Commands.User.RefreshTokenLogin;
using ExpenseTrackingSystem.Application.Features.Commands.User.VerifyResetToken;
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

		[HttpPost("[action]")]
		public async Task<IActionResult> RefreshTokenLogin(RefreshTokenLoginCommandRequest refreshTokenLoginCommandRequest)
		{
			RefreshTokenLoginCommandResponse response = await _mediator.Send(refreshTokenLoginCommandRequest);
			return Ok(response);
		}

		[HttpPost("verify-reset-token")]
		public async Task<IActionResult> VerifyResetToken([FromBody] VerifyResetTokenCommandRequest verifyResetTokenCommandRequest)
		{
			VerifyResetTokenCommandResponse response = await _mediator.Send(verifyResetTokenCommandRequest);
			return Ok(response);
		}
	}
}
