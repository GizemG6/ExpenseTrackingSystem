using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Dtos.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
	{
		private readonly IUserService _userService;

		public CreateUserCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
		{
			UserCreateResponseDto response = await _userService.CreateAsync(new()
			{
				Email = request.Email,
				FullName = request.FullName,
				IBAN = request.IBAN,
				Password = request.Password,
				CreatedDate = request.CreatedDate,
				PhoneNumber = request.PhoneNumber,
				Title = request.Title
			});

			return new()
			{
				Message = response.Message,
				Succeeded = response.Succeeded
			};
		}
	}
}
