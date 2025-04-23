using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Features.Queries.ExpenseCategory.GetAll
{
	public class GetAllExpenseCategoriesQueryRequest : IRequest<List<GetAllExpenseCategoriesQueryResponse>>
	{
	}
}
