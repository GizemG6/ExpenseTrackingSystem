using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Commands.ExpenseCategory.Update
{
	public class UpdateExpenseCategoryCommandRequest : IRequest<UpdateExpenseCategoryCommandResponse>
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
