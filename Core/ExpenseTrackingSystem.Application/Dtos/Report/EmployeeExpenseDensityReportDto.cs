using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Dtos.Report
{
	public class EmployeeExpenseDensityReportDto
	{
		public string FullName { get; set; }
		public decimal TotalAmount { get; set; }
		public int ExpenseCount { get; set; }
	}
}
