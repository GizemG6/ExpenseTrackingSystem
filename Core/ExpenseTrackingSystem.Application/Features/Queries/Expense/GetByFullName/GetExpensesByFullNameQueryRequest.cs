using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.Expense.GetByFullName
{
	public class GetExpensesByFullNameQueryRequest : IRequest<List<GetExpensesByFullNameQueryResponse>>
	{
		public string FullName { get; set; }
	}
}
