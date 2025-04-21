using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.DeleteUser
{
	public class DeleteUserCommandRequest : IRequest<DeleteUserCommandResponse>
	{
		public string Id { get; set; }
	}
}
