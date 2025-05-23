﻿using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.LoginUser
{
	public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
	{
		private readonly IAuthService _authService;

		public LoginUserCommandHandler(IAuthService authService)
		{
			_authService = authService;
		}

		public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
		{
			var token = await _authService.LoginAsync(request.Email, request.Password, 900);
			return new LoginUserSuccessCommandResponse()
			{
				Token = token
			};
		}
	}
}
