using ExpenseTrackingSystem.Application.Dtos.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.RefreshTokenLogin
{
	public class RefreshTokenLoginCommandResponse
	{
		public TokenDto Token { get; set; }
	}
}
