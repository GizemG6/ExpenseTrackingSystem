using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Dtos.Token;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.RefreshTokenLogin
{
	public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
	{
		private IAuthService _authService;

		public RefreshTokenLoginCommandHandler(IAuthService authService)
		{
			_authService = authService;
		}
		public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
		{
			TokenDto token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
			return new()
			{
				Token = token
			};
		}
	}
}
