using ExpenseTrackingSystem.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.User.GetUserById
{
	public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQueryRequest, GetUserByIdQueryResponse>
	{
		private readonly IUserService _userService;

		public GetUserByIdQueryHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<GetUserByIdQueryResponse> Handle(GetUserByIdQueryRequest request, CancellationToken cancellationToken)
		{
			var user = await _userService.GetUserByIdAsync(request.Id);
			return new()
			{
				FullName = user.FullName,
				UserName = user.UserName,
				Email = user.Email,
				PhoneNumber = user.PhoneNumber,
				IBAN = user.IBAN,
				IsActive = user.IsActive,
				CreatedDate = user.CreatedDate
			};
		}
	}
}
