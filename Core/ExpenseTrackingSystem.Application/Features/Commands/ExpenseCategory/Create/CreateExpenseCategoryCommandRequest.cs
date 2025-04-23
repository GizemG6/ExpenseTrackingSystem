using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.ExpenseCategory.Create
{
	public class CreateExpenseCategoryCommandRequest : IRequest<CreateExpenseCategoryCommandResponse>
	{
		public string Name { get; set; }
	}
}
