using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.ExpenseCategory.GetById
{
	public class GetExpenseCategoryByIdQueryRequest : IRequest<GetExpenseCategoryByIdQueryResponse>
	{
		public int Id { get; set; }
	}
}
