using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.UpdateUserByIban
{
	public class UpdateUserByIbanCommandRequest : IRequest<UpdateUserByIbanCommandResponse>
	{
		public string Id { get; set; }
		public string Iban { get; set; }
	}
}
