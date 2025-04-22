using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.UpdatePassword
{
	public class UpdatePasswordCommandRequest : IRequest<UpdatePasswordCommandResponse>
	{
		public string Id { get; set; }
		public string ResetToken { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }
	}
}
