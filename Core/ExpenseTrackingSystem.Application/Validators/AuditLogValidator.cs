using ExpenseTrackingSystem.Application.Dtos.AuditLog;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Validators
{
	public class AuditLogValidator : AbstractValidator<AuditLogDto>
	{
		public AuditLogValidator()
		{
			RuleFor(x => x.Action)
				.NotEmpty();

			RuleFor(x => x.Entity)
				.NotEmpty();

			RuleFor(x => x.EntityId)
				.NotEmpty();
		}
	}
}
