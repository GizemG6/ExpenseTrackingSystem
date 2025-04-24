using ExpenseTrackingSystem.Application.Dtos.Expense;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Validators
{
	public class ExpenseValidator : AbstractValidator<ExpenseCreateDto>
	{
		public ExpenseValidator()
		{
			RuleFor(x => x.UserId)
				.NotEmpty();

			RuleFor(x => x.CategoryId)
				.GreaterThan(0);

			RuleFor(x => x.Amount)
				.GreaterThan(0);

			RuleFor(x => x.Date)
				.NotEmpty()
				.LessThanOrEqualTo(DateTime.Now);

			RuleFor(x => x.Location)
				.NotEmpty()
				.MaximumLength(100);
		}
	}
}
