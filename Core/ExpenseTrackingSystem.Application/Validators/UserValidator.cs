using ExpenseTrackingSystem.Application.Dtos.User;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Validators
{
	public class UserValidator : AbstractValidator<UserCreateDto>
	{
		public UserValidator()
		{
			RuleFor(x => x.FullName)
			.NotEmpty()
			.MaximumLength(100);

			RuleFor(x => x.Email)
				.NotEmpty()
				.EmailAddress();

			RuleFor(x => x.Password)
				.NotEmpty()
				.MinimumLength(6);

			RuleFor(x => x.IBAN)
				.NotEmpty()
				.Matches(@"^TR\d{2}\d{5}\d{16}$");
		}
	}
}
