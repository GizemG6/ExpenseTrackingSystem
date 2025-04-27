using ExpenseTrackingSystem.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Dtos.Report
{
    public class EmployeeRequestReportDto
    {
        public Guid ExpenseId { get; set; }
        public decimal Amount { get; set; }
        public string CategoryName { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        public ExpenseStatus Status { get; set; }
        public string? RejectionReason { get; set; }
        public string? ReceiptFilePath { get; set; }
    }
}
