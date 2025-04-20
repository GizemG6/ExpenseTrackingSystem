using ExpenseTrackingSystem.Application.Dtos.PaymentSimulation;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Validators
{
	public class PaymentSimulationValidator : AbstractValidator<PaymentSimulationCreateDto>
	{
		public PaymentSimulationValidator()
		{
			RuleFor(x => x.ExpenseId)
				.NotEmpty();

			RuleFor(x => x.PaymentDate)
				.NotEmpty()
				.GreaterThan(DateTime.Now.AddDays(-1));

			RuleFor(x => x.BankReferenceNo)
				.NotEmpty()
				.Matches(@"^[A-Za-z0-9]+$");

			RuleFor(x => x.PaidAmount)
				.NotEmpty()
				.GreaterThan(0);

			RuleFor(x => x.IBAN)
				.NotEmpty()
				.Matches(@"^TR\d{2}\d{5}\d{16}$");
		}
	}
}
