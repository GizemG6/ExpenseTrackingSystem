using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.UpdatePassword
{
	public class UpdatePasswordCommandHandler : IRequestHandler<UpdatePasswordCommandRequest, UpdatePasswordCommandResponse>
	{
		private readonly IUserService _userService;

		public UpdatePasswordCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<UpdatePasswordCommandResponse> Handle(UpdatePasswordCommandRequest request, CancellationToken cancellationToken)
		{
			if (!request.Password.Equals(request.PasswordConfirm))
				throw new Exception("Please verify the password exactly.");

			await _userService.UpdatePasswordAsync(request.Id, request.ResetToken, request.Password);
			return new();
		}
	}
}
