using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Domain.Entities.Identity
{
	public class AppUser : IdentityUser
	{
		public string FullName { get; set; }
		public string IBAN { get; set; }
		public bool IsActive { get; set; }
		public DateTime CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }

		public ICollection<Expense> Expenses { get; set; }
	}
}
