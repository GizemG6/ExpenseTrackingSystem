using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Dtos.Report
{
	public class ApprovalStatusReportDto
	{
		public decimal ApprovedAmount { get; set; } 
		public decimal RejectedAmount { get; set; }
		public int ApprovedCount { get; set; }
		public int RejectedCount { get; set; }
	}
}
