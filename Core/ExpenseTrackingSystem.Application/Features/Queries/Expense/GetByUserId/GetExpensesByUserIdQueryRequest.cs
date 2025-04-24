using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.Expense.GetByUserId
{
	public class GetExpensesByUserIdQueryRequest : IRequest<List<GetExpensesByUserIdQueryResponse>>
	{
		public string UserId { get; set; }
	}
}
