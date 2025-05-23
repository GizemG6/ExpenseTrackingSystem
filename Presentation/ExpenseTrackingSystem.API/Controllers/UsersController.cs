﻿using ExpenseTrackingSystem.Application.Features.Commands.User.AssignRoleToUser;
using ExpenseTrackingSystem.Application.Features.Commands.User.CreateUser;
using ExpenseTrackingSystem.Application.Features.Commands.User.DeleteUser;
using ExpenseTrackingSystem.Application.Features.Commands.User.UpdatePassword;
using ExpenseTrackingSystem.Application.Features.Commands.User.UpdateUserByIban;
using ExpenseTrackingSystem.Application.Features.Commands.User.UpdateUserByTitle;
using ExpenseTrackingSystem.Application.Features.Queries.User.GetAllUsers;
using ExpenseTrackingSystem.Application.Features.Queries.User.GetUserById;
using ExpenseTrackingSystem.Application.Features.Queries.User.GetUsersByRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetAllUsers()
		{
			var response = await _mediator.Send(new GetAllUsersQueryRequest());
			return Ok(response);
		}

		[HttpGet("{Id}")]
		public async Task<IActionResult> GetUserById([FromRoute]GetUserByIdQueryRequest getUserByIdQueryRequest)
		{
			GetUserByIdQueryResponse response = await _mediator.Send(getUserByIdQueryRequest);
			return Ok(response);
		}

		[HttpGet("role/{RoleName}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> GetUsersByRole([FromRoute]GetUsersByRoleQueryRequest getUsersByRoleQueryRequest)
		{
			List<GetUsersByRoleQueryResponse> response = await _mediator.Send(getUsersByRoleQueryRequest);
			return Ok(response);
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
		{
			CreateUserCommandResponse response = await _mediator.Send(createUserCommandRequest);
			return Ok(response);
		}

		[HttpPost("assign-role")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> AssignRole([FromBody] AssignRoleToUserCommandRequest request)
		{
			AssignRoleToUserCommandResponse response = await _mediator.Send(request);
			return Ok(response);
		}

		[HttpPost("update-password")]
		public async Task<IActionResult> UpdatePassword([FromBody] UpdatePasswordCommandRequest updatePasswordCommandRequest)
		{
			UpdatePasswordCommandResponse response = await _mediator.Send(updatePasswordCommandRequest);
			return Ok(response);
		}

		[HttpPut("update-title")]
		public async Task<IActionResult> UpdateUserTitle(UpdateUserByTitleCommandRequest updateUserByTitleCommandRequest)
		{
			UpdateUserByTitleCommandResponse response = await _mediator.Send(updateUserByTitleCommandRequest);
			return Ok(response);
		}

		[HttpPut("update-IBAN")]
		public async Task<IActionResult> UpdateUserIban(UpdateUserByIbanCommandRequest updateUserByIbanCommandRequest)
		{
			UpdateUserByIbanCommandResponse response = await _mediator.Send(updateUserByIbanCommandRequest);
			return Ok(response);
		}

		[HttpDelete("{Id}")]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteUser([FromRoute]DeleteUserCommandRequest deleteUserCommandRequest)
		{
			DeleteUserCommandResponse response = await _mediator.Send(deleteUserCommandRequest);
			return Ok(response);
		}
	}
}
