using Dapper;
using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Dtos.AuditLog;
using ExpenseTrackingSystem.Application.Dtos.Report;
using ExpenseTrackingSystem.Domain.Entities;
using ExpenseTrackingSystem.Persistence.Context;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Persistence.Services
{
	public class AuditLogService : IAuditLogService
	{
		private readonly AppDbContext _context;
		private readonly ILogger<AuditLogService> _logger;
		private readonly IConnectionMultiplexer _redisConnection;
		private readonly IConfiguration _configuration;

		public AuditLogService(AppDbContext context, ILogger<AuditLogService> logger, IConnectionMultiplexer connectionMultiplexer, IConfiguration configuration)
		{
			_context = context;
			_logger = logger;
			_redisConnection = connectionMultiplexer;
			_configuration = configuration;
		}

		public async Task<IEnumerable<AuditLogDto>> GetAuditLogsAsync(string userId = null, DateTime? startDate = null, DateTime? endDate = null)
		{
			var cacheKey = $"AuditLogs_{userId}_{startDate?.ToString("yyyyMMdd")}_{endDate?.ToString("yyyyMMdd")}";

			var db = _redisConnection.GetDatabase();
			var cachedData = await db.StringGetAsync(cacheKey);
			if (cachedData.HasValue)
			{
				return JsonConvert.DeserializeObject<IEnumerable<AuditLogDto>>(cachedData);
			}
			using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

			var result = await connection.QueryAsync<AuditLogDto>(GetAuditLogsSql, new { UserId = userId, StartDate = startDate, EndDate = endDate });

			await db.StringSetAsync(cacheKey, JsonConvert.SerializeObject(result), TimeSpan.FromMinutes(5));

			return result;
		}

		public async Task LogActionAsync(string userId, string action, string entity, string entityId)
		{
			var auditLog = new AuditLog
			{
				Id = Guid.NewGuid(),
				UserId = userId,
				Action = action,
				Entity = entity,
				EntityId = entityId,
				ActionDate = DateTime.UtcNow
			};

			await _context.AuditLogs.AddAsync(auditLog);
			await _context.SaveChangesAsync();

			_logger.LogInformation("Audit log created for user {UserId}, action: {Action}, entity: {Entity}, entityId: {EntityId}",
								   userId, action, entity, entityId);
		}

		private const string GetAuditLogsSql = @"
				SELECT Id, UserId, Action, Entity, EntityId, ActionDate
				FROM AuditLogs
				WHERE (@UserId IS NULL OR UserId = @UserId)
				AND (@StartDate IS NULL OR ActionDate >= @StartDate)
				AND (@EndDate IS NULL OR ActionDate <= @EndDate)
				ORDER BY ActionDate DESC";
	}
}

