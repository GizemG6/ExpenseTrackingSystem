using ExpenseTrackingSystem.Application.Dtos.ExpenseCategory;
using ExpenseTrackingSystem.Application.Features.Commands.ExpenseCategory.Create;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Validators
{
	public class ExpenseCategoryValidator : AbstractValidator<CreateExpenseCategoryCommandRequest>
	{
		public ExpenseCategoryValidator()
		{
			RuleFor(x => x.Name)
			.NotEmpty()
			.MaximumLength(100);
		}
	}
}
