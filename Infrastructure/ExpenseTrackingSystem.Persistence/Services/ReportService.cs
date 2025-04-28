using Dapper;
using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Dtos.Report;
using ExpenseTrackingSystem.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Persistence.Services
{
    public class ReportService : IReportService
	{
		private readonly IConfiguration _configuration;
		private readonly IConnectionMultiplexer _redisConnection;

		public ReportService(IConfiguration configuration, IConnectionMultiplexer connectionMultiplexer)
		{
			_configuration = configuration;
			_redisConnection = connectionMultiplexer;
		}

		public async Task<IEnumerable<EmployeeRequestReportDto>> GetEmployeeRequestsAsync(string userId)
		{
			var cacheKey = $"EmployeeRequests_{userId}";
			var db = _redisConnection.GetDatabase();

			var cachedData = await db.StringGetAsync(cacheKey);
			if (cachedData.HasValue)
			{
				return JsonConvert.DeserializeObject<IEnumerable<EmployeeRequestReportDto>>(cachedData);
			}

			using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

			var result = await connection.QueryAsync<EmployeeRequestReportDto>(EmployeeRequestReportSql, new { UserId = userId });

			await db.StringSetAsync(cacheKey, JsonConvert.SerializeObject(result), TimeSpan.FromMinutes(120));

			return result;
		}

		public async Task<IEnumerable<CompanyPaymentDensityReportDto>> GetCompanyPaymentDensityAsync(DateTime startDate, DateTime endDate, string reportType)
		{
			var cacheKey = $"CompanyPaymentDensity_{reportType}_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}";
			var db = _redisConnection.GetDatabase();

			var cachedData = await db.StringGetAsync(cacheKey);
			if (cachedData.HasValue)
			{
				return JsonConvert.DeserializeObject<IEnumerable<CompanyPaymentDensityReportDto>>(cachedData);
			}
			using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

			string sqlQuery = reportType.ToLower() switch
			{
				"daily" => DailyPaymentDensityQuery,
				"weekly" => WeeklyPaymentDensityQuery,
				"monthly" => MonthlyPaymentDensityQuery,
				_ => throw new ArgumentException("Invalid report type. Valid types are: daily, weekly, monthly.")
			};

			var parameters = new { StartDate = startDate, EndDate = endDate };
			var result = await connection.QueryAsync<CompanyPaymentDensityReportDto>(sqlQuery, parameters);

			await db.StringSetAsync(cacheKey, JsonConvert.SerializeObject(result), TimeSpan.FromMinutes(120));


			return result;
		}

		public async Task<IEnumerable<EmployeeExpenseDensityReportDto>> GetEmployeeExpenseDensityAsync(DateTime startDate, DateTime endDate, string reportType)
		{
			var cacheKey = $"EmployeeExpenseDensity_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}_{reportType.ToLower()}";
			var db = _redisConnection.GetDatabase();

			var cachedData = await db.StringGetAsync(cacheKey);
			if (cachedData.HasValue)
			{
				return JsonConvert.DeserializeObject<IEnumerable<EmployeeExpenseDensityReportDto>>(cachedData);
			}

			using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

			string sqlQuery = reportType.ToLower() switch
			{
				"daily" => DailyEmployeeExpenseDensityQuery,
				"weekly" => WeeklyEmployeeExpenseDensityQuery,
				"monthly" => MonthlyEmployeeExpenseDensityQuery,
				_ => throw new ArgumentException("Invalid report type. Valid types are: daily, weekly, monthly.")
			};

			var parameters = new { StartDate = startDate, EndDate = endDate };

			var result = await connection.QueryAsync<EmployeeExpenseDensityReportDto>(sqlQuery, parameters);

			await db.StringSetAsync(cacheKey, JsonConvert.SerializeObject(result), TimeSpan.FromMinutes(120));

			return result;
		}

		public async Task<IEnumerable<ApprovalStatusReportDto>> GetExpenseApprovalStatusAsync(DateTime startDate, DateTime endDate, string reportType)
		{
			var cacheKey = $"ExpenseApprovalStatus_{startDate:yyyyMMdd}_{endDate:yyyyMMdd}_{reportType.ToLower()}";
			var db = _redisConnection.GetDatabase();
			var cachedData = await db.StringGetAsync(cacheKey);
			if (cachedData.HasValue)
			{
				return JsonConvert.DeserializeObject<IEnumerable<ApprovalStatusReportDto>>(cachedData);
			}
			using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

			string sqlQuery = reportType.ToLower() switch
			{
				"daily" => DailyExpenseApprovalStatusQuery,
				"weekly" => WeeklyExpenseApprovalStatusQuery,
				"monthly" => MonthlyExpenseApprovalStatusQuery,
				_ => throw new ArgumentException("Invalid report type. Valid types are: daily, weekly, monthly.")
			};

			var parameters = new
			{
				Approved = ExpenseStatus.Approved,
				Rejected = ExpenseStatus.Rejected,
				StartDate = startDate,
				EndDate = endDate
			};
			var result = await connection.QueryAsync<ApprovalStatusReportDto>(sqlQuery, parameters);

			await db.StringSetAsync(cacheKey, JsonConvert.SerializeObject(result), TimeSpan.FromMinutes(120));

			return result;
		}

		private const string EmployeeRequestReportSql = @"
			SELECT 
				e.Id AS ExpenseId,
				e.Amount,
				c.Name AS CategoryName,
				e.Date,
				e.Location,
				e.Status,
				e.RejectionReason,
				e.ReceiptFilePath
			FROM Expenses e
			INNER JOIN ExpenseCategories c ON e.CategoryId = c.Id
			INNER JOIN AspNetUsers u ON e.UserId = u.Id
			INNER JOIN AspNetUserRoles ur ON u.Id = ur.UserId
			INNER JOIN AspNetRoles r ON ur.RoleId = r.Id
			WHERE e.UserId = @UserId
			  AND r.Name = 'Employee'
			ORDER BY e.Date DESC";

		private const string DailyPaymentDensityQuery = @"
			SELECT 
				SUM(PaidAmount) AS TotalAmount,
				COUNT(*) AS PaymentCount
			FROM PaymentSimulations
			WHERE CAST(PaymentDate AS DATE) BETWEEN @StartDate AND @EndDate
			GROUP BY CAST(PaymentDate AS DATE)
			ORDER BY CAST(PaymentDate AS DATE)";

		private const string WeeklyPaymentDensityQuery = @"
			SELECT 
				SUM(PaidAmount) AS TotalAmount,
				COUNT(*) AS PaymentCount
			FROM PaymentSimulations
			WHERE PaymentDate BETWEEN @StartDate AND @EndDate
			GROUP BY DATEPART(YEAR, PaymentDate), DATEPART(WEEK, PaymentDate)
			ORDER BY DATEPART(YEAR, PaymentDate), DATEPART(WEEK, PaymentDate)";

		private const string MonthlyPaymentDensityQuery = @"
			SELECT 
				SUM(PaidAmount) AS TotalAmount,
				COUNT(*) AS PaymentCount
			FROM PaymentSimulations
			WHERE PaymentDate BETWEEN @StartDate AND @EndDate
			GROUP BY YEAR(PaymentDate), MONTH(PaymentDate)
			ORDER BY YEAR(PaymentDate), MONTH(PaymentDate)";

		private const string DailyEmployeeExpenseDensityQuery = @"
			SELECT 
				u.FullName, 
				SUM(e.Amount) AS TotalAmount,
				COUNT(*) AS ExpenseCount
			FROM Expenses e
			INNER JOIN AspNetUsers u ON u.Id = e.UserId
			WHERE e.Date BETWEEN @StartDate AND @EndDate
			GROUP BY u.FullName, CAST(e.Date AS DATE)
			ORDER BY u.FullName";

		private const string WeeklyEmployeeExpenseDensityQuery = @"
			SELECT 
				u.FullName, 
				SUM(e.Amount) AS TotalAmount,
				COUNT(*) AS ExpenseCount
			FROM Expenses e
			INNER JOIN AspNetUsers u ON u.Id = e.UserId
			WHERE e.Date BETWEEN @StartDate AND @EndDate
			GROUP BY u.FullName, DATEPART(YEAR, e.Date), DATEPART(WEEK, e.Date)
			ORDER BY u.FullName";

		private const string MonthlyEmployeeExpenseDensityQuery = @"
			SELECT 
				u.FullName, 
				SUM(e.Amount) AS TotalAmount,
				COUNT(*) AS ExpenseCount
			FROM Expenses e
			INNER JOIN AspNetUsers u ON u.Id = e.UserId
			WHERE e.Date BETWEEN @StartDate AND @EndDate
			GROUP BY u.FullName, YEAR(e.Date), MONTH(e.Date)
			ORDER BY u.FullName";

		private const string DailyExpenseApprovalStatusQuery = @"
			SELECT 
				CAST(e.Date AS DATE) AS Date, 
				SUM(CASE WHEN e.Status = @Approved THEN e.Amount ELSE 0 END) AS ApprovedAmount,
				SUM(CASE WHEN e.Status = @Rejected THEN e.Amount ELSE 0 END) AS RejectedAmount,
				COUNT(CASE WHEN e.Status = @Approved THEN 1 END) AS ApprovedCount,
				COUNT(CASE WHEN e.Status = @Rejected THEN 1 END) AS RejectedCount
			FROM Expenses e
			WHERE e.Date BETWEEN @StartDate AND @EndDate
			GROUP BY CAST(e.Date AS DATE)
			ORDER BY Date";

		private const string WeeklyExpenseApprovalStatusQuery = @"
			SELECT 
				DATEPART(YEAR, e.Date) AS Year,
				DATEPART(WEEK, e.Date) AS WeekNumber,
				SUM(CASE WHEN e.Status = @Approved THEN e.Amount ELSE 0 END) AS ApprovedAmount,
				SUM(CASE WHEN e.Status = @Rejected THEN e.Amount ELSE 0 END) AS RejectedAmount,
				COUNT(CASE WHEN e.Status = @Approved THEN 1 END) AS ApprovedCount,
				COUNT(CASE WHEN e.Status = @Rejected THEN 1 END) AS RejectedCount
			FROM Expenses e
			WHERE e.Date BETWEEN @StartDate AND @EndDate
			GROUP BY DATEPART(YEAR, e.Date), DATEPART(WEEK, e.Date)
			ORDER BY Year, WeekNumber";

		private const string MonthlyExpenseApprovalStatusQuery = @"
			SELECT 
				YEAR(e.Date) AS Year,
				MONTH(e.Date) AS Month,
				SUM(CASE WHEN e.Status = @Approved THEN e.Amount ELSE 0 END) AS ApprovedAmount,
				SUM(CASE WHEN e.Status = @Rejected THEN e.Amount ELSE 0 END) AS RejectedAmount,
				COUNT(CASE WHEN e.Status = @Approved THEN 1 END) AS ApprovedCount,
				COUNT(CASE WHEN e.Status = @Rejected THEN 1 END) AS RejectedCount
			FROM Expenses e
			WHERE e.Date BETWEEN @StartDate AND @EndDate
			GROUP BY YEAR(e.Date), MONTH(e.Date)
			ORDER BY Year, Month";
	}
}
