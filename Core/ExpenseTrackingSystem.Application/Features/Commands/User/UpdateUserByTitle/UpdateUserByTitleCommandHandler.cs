using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Dtos.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.UpdateUserByTitle
{
	public class UpdateUserByTitleCommandHandler : IRequestHandler<UpdateUserByTitleCommandRequest, UpdateUserByTitleCommandResponse>
	{
		private readonly IUserService _userService;

		public UpdateUserByTitleCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<UpdateUserByTitleCommandResponse> Handle(UpdateUserByTitleCommandRequest request, CancellationToken cancellationToken)
		{
			var result = await _userService.UpdateUserByTitleAsync(new UpdateUserTitleDto
			{
				Id = request.Id,
				Title = request.Title
			});

			return new UpdateUserByTitleCommandResponse
			{
				Success = result,
				Message = result ? "User title updated." : "User not found or update failed."
			};
		}
	}
}
