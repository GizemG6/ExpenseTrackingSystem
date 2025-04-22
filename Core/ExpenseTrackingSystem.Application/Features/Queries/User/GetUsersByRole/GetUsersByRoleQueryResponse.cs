using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.User.GetUsersByRole
{
	public class GetUsersByRoleQueryResponse
	{
		public string FullName { get; set; }
		public string UserName { get; set; }
		public string Title { get; set; }
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
		public string IBAN { get; set; }

		public bool IsActive { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}
