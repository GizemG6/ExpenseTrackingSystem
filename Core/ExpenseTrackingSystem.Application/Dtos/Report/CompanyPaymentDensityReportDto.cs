using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Dtos.Report
{
    public class CompanyPaymentDensityReportDto
    {
        public decimal TotalAmount { get; set; }
        public int PaymentCount { get; set; }
    }
}
