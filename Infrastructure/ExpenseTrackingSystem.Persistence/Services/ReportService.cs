using Dapper;
using ExpenseTrackingSystem.Application.Abstractions.Services;
using ExpenseTrackingSystem.Application.Dtos.Report;
using ExpenseTrackingSystem.Domain.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
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

		public ReportService(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task<IEnumerable<EmployeeRequestReportDto>> GetEmployeeRequestsAsync(string userId)
		{
			using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

			var sql = @"
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

			return await connection.QueryAsync<EmployeeRequestReportDto>(sql, new { UserId = userId });
		}

		public async Task<IEnumerable<CompanyPaymentDensityReportDto>> GetCompanyPaymentDensityAsync(DateTime startDate, DateTime endDate)
		{
			using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

			var sqlQuery = @"
				SELECT 
					CAST(PaymentDate AS DATE) AS Date, 
					SUM(PaidAmount) AS TotalAmount,
					COUNT(*) AS PaymentCount
				FROM PaymentSimulations
				WHERE PaymentDate BETWEEN @StartDate AND @EndDate
				GROUP BY CAST(PaymentDate AS DATE)
				ORDER BY Date";

			var parameters = new { StartDate = startDate, EndDate = endDate };

			return await connection.QueryAsync<CompanyPaymentDensityReportDto>(sqlQuery, parameters);
		}

		public async Task<IEnumerable<EmployeeExpenseDensityReportDto>> GetEmployeeExpenseDensityAsync(DateTime startDate, DateTime endDate)
		{
			using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

			var sqlQuery = @"
				SELECT 
					u.FullName, 
					CAST(e.Date AS DATE) AS Date, 
					SUM(e.Amount) AS TotalAmount,
					COUNT(*) AS ExpenseCount
				FROM Expenses e
				INNER JOIN AppUsers u ON u.Id = e.UserId
				WHERE e.Date BETWEEN @StartDate AND @EndDate
				GROUP BY u.FullName, CAST(e.Date AS DATE)
				ORDER BY Date, u.FullName";

			var parameters = new { StartDate = startDate, EndDate = endDate };

			return await connection.QueryAsync<EmployeeExpenseDensityReportDto>(sqlQuery, parameters);
		}

		public async Task<IEnumerable<ApprovalStatusReportDto>> GetExpenseApprovalStatusAsync(DateTime startDate, DateTime endDate, string reportType)
		{
			using var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));

			string sqlQuery = string.Empty;

			if (reportType == "daily")
			{
				sqlQuery = @"
					SELECT 
						CAST(e.Date AS DATE) AS Date, 
						SUM(CASE WHEN e.Status = @Approved THEN e.Amount ELSE 0 END) AS ApprovedAmount,
						SUM(CASE WHEN e.Status = @Rejected THEN e.Amount ELSE 0 END) AS RejectedAmount,
						COUNT(CASE WHEN e.Status = @Approved THEN 1 END) AS ApprovedCount,
						COUNT(CASE WHEN e.Status = @Rejected THEN 1 END) AS RejectedCount
					FROM Expenses e
					WHERE CAST(e.Date AS DATE) BETWEEN @StartDate AND @EndDate
					GROUP BY CAST(e.Date AS DATE)
					ORDER BY Date";
			}
			else if (reportType == "weekly")
			{
				sqlQuery = @"
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
			}
			else if (reportType == "monthly")
			{
				sqlQuery = @"
					SELECT 
						YEAR(e.Date) AS Year,
						MONTH(e.Date) AS Month,
						SUM(CASE WHEN e.Status = @Approved THEN e.Amount ELSE 0 END) AS ApprovedAmount,
						SUM(CASE WHEN e.Status = @Rejected THEN e.Amount ELSE 0 END) AS RejectedAmount,
						COUNT(CASE WHEN e.Status = @Approved THEN 1 END) AS ApprovedCount,
						COUNT(CASE WHEN e.Status = @Rejected THEN 1 END) AS RejectedCount
					FROM Expenses e
					WHERE CAST(e.Date AS DATE) BETWEEN @StartDate AND @EndDate
					GROUP BY YEAR(e.Date), MONTH(e.Date)
					ORDER BY Year, Month";
			}

			var parameters = new
			{
				Approved = ExpenseStatus.Approved,
				Rejected = ExpenseStatus.Rejected,
				StartDate = startDate,
				EndDate = endDate
			};

			return await connection.QueryAsync<ApprovalStatusReportDto>(sqlQuery, parameters);
		}
	}
}
