﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Domain.Entities
{
	public class PaymentSimulation
	{
		public Guid Id { get; set; }
		public DateTime PaymentDate { get; set; }
		public string BankReferenceNo { get; set; }
		public Expense Expense { get; set; }

		public decimal PaidAmount { get; set; }
		public string SenderFullName { get; set; }
		public string SenderIban { get; set; }

		public string ReceiverFullName { get; set; }
		public string ReceiverIban { get; set; }
	}
}
