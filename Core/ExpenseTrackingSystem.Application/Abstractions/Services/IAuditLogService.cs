using ExpenseTrackingSystem.Application.Dtos.AuditLog;
using ExpenseTrackingSystem.Application.Dtos.PaymentSimulation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Abstractions.Services
{
	public interface IAuditLogService
	{
		Task<IEnumerable<AuditLogDto>> GetAuditLogsAsync(string userId = null, DateTime? startDate = null, DateTime? endDate = null);
		Task LogActionAsync(string userId, string action, string entity, string entityId);
	}
}
