﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.AssignRoleToUser
{
	public class AssignRoleToUserCommandRequest : IRequest<AssignRoleToUserCommandResponse>
	{
		public string Id { get; set; }
		public string Role { get; set; }
	}
}
