using ExpenseTrackingSystem.Application.Dtos.ExpenseCategory;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Validators
{
	public class ExpenseCategoryValidator : AbstractValidator<ExpenseCategoryCreateDto>
	{
		public ExpenseCategoryValidator()
		{
			RuleFor(x => x.Name)
			.NotEmpty()
			.MaximumLength(100);
		}
	}
}
