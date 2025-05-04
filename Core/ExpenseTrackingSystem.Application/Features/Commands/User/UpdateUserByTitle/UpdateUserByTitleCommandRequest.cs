using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.UpdateUserByTitle
{
	public class UpdateUserByTitleCommandRequest : IRequest<UpdateUserByTitleCommandResponse>
	{
		public string Id { get; set; }
		public string Title { get; set; }
	}
}
