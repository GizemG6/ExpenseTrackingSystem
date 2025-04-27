using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ExpenseTrackingSystem.Application.Dtos.Report
{
	public class EmployeeExpenseDensityReportDto
	{
		public string FullName { get; set; }
		public DateTime Date { get; set; } 
		public decimal TotalAmount { get; set; }
		public int ExpenseCount { get; set; }

		public DateTime ValidatedDate => Date == DateTime.MinValue ? DateTime.Now : Date;
	}
}
