using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Dtos.User;
using ExpenseTrackingSystem.Application.Features.Commands.User.UpdateUserByTitle;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.UpdateUserByIban
{
	public class UpdateUserByIbanCommandHandler : IRequestHandler<UpdateUserByIbanCommandRequest, UpdateUserByIbanCommandResponse>
	{
		private readonly IUserService _userService;

		public UpdateUserByIbanCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<UpdateUserByIbanCommandResponse> Handle(UpdateUserByIbanCommandRequest request, CancellationToken cancellationToken)
		{
			var result = await _userService.UpdateUserIbanAsync(new UpdateUserIbanDto
			{
				Id = request.Id,
				Iban = request.Iban
			});

			return new UpdateUserByIbanCommandResponse
			{
				Success = result,
				Message = result ? "User IBAN updated." : "User not found or update failed."
			};
		}
	}
}
