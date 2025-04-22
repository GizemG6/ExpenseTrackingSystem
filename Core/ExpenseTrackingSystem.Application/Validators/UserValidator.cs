using ExpenseTrackingSystem.Application.Dtos.User;
using ExpenseTrackingSystem.Application.Features.Commands.User.CreateUser;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Validators
{
	public class UserValidator : AbstractValidator<CreateUserCommandRequest>
	{
		public UserValidator()
		{
			RuleFor(x => x.FullName)
			.NotEmpty()
			.MaximumLength(100);

			RuleFor(x => x.Title)
			.NotEmpty()
			.MaximumLength(100);

			RuleFor(x => x.Email)
				.NotEmpty()
				.EmailAddress();

			RuleFor(x => x.Password)
				.NotEmpty()
				.MinimumLength(6)
				.Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$");

			RuleFor(x => x.IBAN)
				.NotEmpty()
				.Matches(@"^TR\d{2}\d{5}\d{16}$");

			RuleFor(x => x.PhoneNumber)
			.NotEmpty()
			.Matches(@"^(?:\+90|0)?5\d{9}$");
		}
	}
}
