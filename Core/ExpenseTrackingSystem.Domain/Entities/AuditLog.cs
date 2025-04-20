﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Domain.Entities
{
	public class AuditLog
	{
		public Guid Id { get; set; }
		public string UserId { get; set; }
		public string Action { get; set; }
		public string Entity { get; set; }
		public string EntityId { get; set; }
		public DateTime ActionDate { get; set; }
	}
}
