using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.PasswordReset
{
	public class PasswordResetCommandHandler : IRequestHandler<PasswordResetCommandRequest, PasswordResetCommandResponse>
	{
		private readonly IAuthService _authService;

		public PasswordResetCommandHandler(IAuthService authService)
		{
			_authService = authService;
		}
		public async Task<PasswordResetCommandResponse> Handle(PasswordResetCommandRequest request, CancellationToken cancellationToken)
		{
			await _authService.PasswordResetAsnyc(request.Email);
			return new();
		}
	}
}
