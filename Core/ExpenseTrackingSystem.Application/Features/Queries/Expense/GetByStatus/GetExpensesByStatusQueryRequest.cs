using ExpenseTrackingSystem.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.Expense.GetByStatus
{
	public class GetExpensesByStatusQueryRequest : IRequest<List<GetExpensesByStatusQueryResponse>>
	{
		public ExpenseStatus Status { get; set; }
	}
}
