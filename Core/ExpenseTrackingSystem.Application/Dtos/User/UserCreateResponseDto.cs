using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Dtos.User
{
	public class UserCreateResponseDto
	{
		public bool Succeeded { get; set; }
		public string Message { get; set; }
	}
}
