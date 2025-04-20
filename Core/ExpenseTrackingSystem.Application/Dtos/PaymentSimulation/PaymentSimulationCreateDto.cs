using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Dtos.PaymentSimulation
{
	public class PaymentSimulationCreateDto
	{
		public int ExpenseId { get; set; }
		public DateTime PaymentDate { get; set; }
		public string BankReferenceNo { get; set; }
		public decimal PaidAmount { get; set; }
		public string IBAN { get; set; }
	}
}
