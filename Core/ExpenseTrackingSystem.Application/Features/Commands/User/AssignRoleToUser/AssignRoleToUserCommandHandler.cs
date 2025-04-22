using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.AssignRoleToUser
{
    public class AssignRoleToUserCommandHandler : IRequestHandler<AssignRoleToUserCommandRequest, AssignRoleToUserCommandResponse>
	{
		private readonly IUserService _userService;

		public AssignRoleToUserCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<AssignRoleToUserCommandResponse> Handle(AssignRoleToUserCommandRequest request, CancellationToken cancellationToken)
		{
			await _userService.AssignRoleToUserAsnyc(request.Id, request.Role);

			return new AssignRoleToUserCommandResponse
			{
				Succeeded = true,
				Message = "Roles assigned successfully."
			};
		}
	}
}
