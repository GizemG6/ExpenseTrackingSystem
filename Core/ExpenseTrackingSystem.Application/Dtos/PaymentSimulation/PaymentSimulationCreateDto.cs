using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Dtos.PaymentSimulation
{
	public class PaymentSimulationCreateDto
	{
		public Guid ExpenseId { get; set; }
		public DateTime PaymentDate { get; set; }
		public string BankReferenceNo { get; set; }
		public decimal PaidAmount { get; set; }

		public string SenderFullName { get; set; }
		public string SenderIban { get; set; }

		public string ReceiverFullName { get; set; }
		public string ReceiverIban { get; set; }
	}
}
