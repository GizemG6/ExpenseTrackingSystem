using ExpenseTrackingSystem.Application.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.User.GetAllUsers
{
	public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQueryRequest, List<GetAllUsersQueryResponse>>
	{
		private readonly IUserService _userService;

		public GetAllUsersQueryHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<List<GetAllUsersQueryResponse>> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
		{
			var users = await _userService.GetAllUsersAsync();
			return users.Select(users => new GetAllUsersQueryResponse
			{
				FullName = users.FullName,
				UserName = users.UserName,
				Email = users.Email,
				PhoneNumber = users.PhoneNumber,
				IBAN = users.IBAN,
				IsActive = users.IsActive,
				CreatedDate = users.CreatedDate
			}).ToList();
		}
	}
}
