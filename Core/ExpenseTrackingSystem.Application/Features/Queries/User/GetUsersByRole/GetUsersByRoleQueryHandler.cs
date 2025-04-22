using ExpenseTrackingSystem.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.User.GetUsersByRole
{
    public class GetUsersByRoleQueryHandler : IRequestHandler<GetUsersByRoleQueryRequest, List<GetUsersByRoleQueryResponse>>
	{
		private readonly IUserService _userService;

		public GetUsersByRoleQueryHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<List<GetUsersByRoleQueryResponse>> Handle(GetUsersByRoleQueryRequest request, CancellationToken cancellationToken)
		{
			var users = await _userService.GetUsersByRoleAsync(request.RoleName);
			return users.Select(u => new GetUsersByRoleQueryResponse
			{
				FullName = u.FullName,
				UserName = u.UserName,
				Email = u.Email,
				PhoneNumber = u.PhoneNumber,
				IBAN = u.IBAN,
				IsActive = u.IsActive,
				CreatedDate = u.CreatedDate
			}).ToList();
		}
	}
}
