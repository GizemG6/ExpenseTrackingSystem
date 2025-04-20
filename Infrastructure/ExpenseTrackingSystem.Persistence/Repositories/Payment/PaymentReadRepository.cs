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
	public class PaymentReadRepository : ReadRepository<PaymentSimulation, Guid>, IPaymentReadRepository
	{
		public PaymentReadRepository(AppDbContext context) : base(context) { }
	}
}
