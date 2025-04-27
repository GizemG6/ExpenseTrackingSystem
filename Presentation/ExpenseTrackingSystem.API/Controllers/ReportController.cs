using ExpenseTrackingSystem.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTrackingSystem.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReportController : ControllerBase
	{
		private readonly IReportService _reportService;

		public ReportController(IReportService reportService)
		{
			_reportService = reportService;
		}

		[HttpGet("personal-requests/{userId}")]
		public async Task<IActionResult> GetPersonalRequests(string userId)
		{
			var result = await _reportService.GetEmployeeRequestsAsync(userId);

			if (result == null || !result.Any())
				return NotFound("No personal requests found for this user.");

			return Ok(result);
		}

		[HttpGet("company-payment-density")]
		public async Task<IActionResult> GetCompanyPaymentDensity([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, string reportType)
		{
			var result = await _reportService.GetCompanyPaymentDensityAsync(startDate, endDate, reportType);

			if (result == null || !result.Any())
				return NotFound("No payment data found for the given date range.");

			return Ok(result);
		}

		[HttpGet("employee-expense-density")]
		public async Task<IActionResult> GetEmployeeExpenseDensity([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
		{
			var result = await _reportService.GetEmployeeExpenseDensityAsync(startDate, endDate);

			if (result == null || !result.Any())
				return NotFound("No expense data found for the given date range.");

			return Ok(result);
		}

		[HttpGet("expense-approval-status")]
		public async Task<IActionResult> GetExpenseApprovalStatus([FromQuery] DateTime startDate, [FromQuery] DateTime endDate, [FromQuery]string reportType)
		{
			var result = await _reportService.GetExpenseApprovalStatusAsync(startDate, endDate, reportType);

			if (result == null || !result.Any())
				return NotFound("No expense approval data found for the given date range.");

			return Ok(result);
		}
	}
}
