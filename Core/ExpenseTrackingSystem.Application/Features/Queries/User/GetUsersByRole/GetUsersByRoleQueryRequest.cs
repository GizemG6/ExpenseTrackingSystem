using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.User.GetUsersByRole
{
	public class GetUsersByRoleQueryRequest : IRequest<List<GetUsersByRoleQueryResponse>>
	{
		public string RoleName { get; set; }
	}
}
