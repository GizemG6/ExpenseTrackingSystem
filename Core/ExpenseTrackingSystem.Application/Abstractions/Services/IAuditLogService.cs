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
		Task<IEnumerable<AuditLogDto>> GetAuditLogsForUserAsync(string userId);
	}
}
