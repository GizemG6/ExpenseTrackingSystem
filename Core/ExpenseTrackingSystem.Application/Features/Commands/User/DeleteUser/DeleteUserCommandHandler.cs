using ExpenseTrackingSystem.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.DeleteUser
{
	public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommandRequest, DeleteUserCommandResponse>
	{
		private readonly IUserService _userService;

		public DeleteUserCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommandRequest request, CancellationToken cancellationToken)
		{
			var user = await _userService.GetUserByIdAsync(request.Id);
			if (user == null)
			{
				throw new Exception("User not found");
			}
			var result = await _userService.DeleteUserAsync(request.Id);
			if (result)
			{
				return new DeleteUserCommandResponse
				{
					Message = "User deleted successfully"
				};
			}
			else
			{
				return new DeleteUserCommandResponse
				{
					Message = "Failed to delete user"
				};
			}
		}
	}
}
