using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.User.UpdateUserByIban
{
	public class UpdateUserByIbanCommandResponse
	{
		public bool Success { get; set; }
		public string Message { get; set; }
	}
}
