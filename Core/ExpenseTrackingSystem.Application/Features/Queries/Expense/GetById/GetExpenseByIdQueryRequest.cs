using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.Expense.GetById
{
	public class GetExpenseByIdQueryRequest : IRequest<GetExpenseByIdQueryResponse>
	{
		public Guid Id { get; set; }
	}
}
