using ExpenseTrackingSystem.Application.Repositories.Payment;
using ExpenseTrackingSystem.Domain.Entities;
using ExpenseTrackingSystem.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Persistence.Repositories.Payment
{
	public class PaymentWriteRepository : WriteRepository<PaymentSimulation, Guid>, IPaymentWriteRepository
	{
		public PaymentWriteRepository(AppDbContext context) : base(context) { }
	}
}
