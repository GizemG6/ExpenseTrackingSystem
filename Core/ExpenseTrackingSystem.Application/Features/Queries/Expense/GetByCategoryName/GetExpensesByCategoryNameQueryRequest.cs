using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.Expense.GetByCategoryName
{
	public class GetExpensesByCategoryNameQueryRequest : IRequest<List<GetExpensesByCategoryNameQueryResponse>>
	{
		public string CategoryName { get; set; }
	}
}
