using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.CreateUser
{
	public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
	{
		public string FullName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string IBAN { get; set; }
		public string PhoneNumber { get; set; }
		public DateTime CreatedDate { get; set; }
	}
}
