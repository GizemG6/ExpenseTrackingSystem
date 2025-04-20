using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Dtos.AuditLog
{
	public class AuditLogDto
	{
		public int Id { get; set; }
		public long UserId { get; set; }
		public string Action { get; set; }
		public string Entity { get; set; }
		public string EntityId { get; set; }
		public DateTime ActionDate { get; set; }
	}
}
