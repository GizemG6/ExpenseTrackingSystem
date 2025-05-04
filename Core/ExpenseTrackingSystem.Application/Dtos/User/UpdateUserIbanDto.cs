using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Dtos.User
{
	public class UpdateUserIbanDto
	{
		public string Id { get; set; }
		public string Iban { get; set; }
	}
}
