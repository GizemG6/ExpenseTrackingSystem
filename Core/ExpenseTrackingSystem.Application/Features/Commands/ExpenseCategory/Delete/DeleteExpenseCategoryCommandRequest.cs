using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.ExpenseCategory.Delete
{
	public class DeleteExpenseCategoryCommandRequest : IRequest<DeleteExpenseCategoryCommandResponse>
	{
		public int Id { get; set; }
	}
}
