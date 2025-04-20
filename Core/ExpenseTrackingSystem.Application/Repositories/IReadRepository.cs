using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Repositories
{
	public interface IReadRepository<T, TKey> where T : class
	{
		Task<List<T>> GetAllAsync(bool tracking = true);
		IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true);
		Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
		Task<T> GetByIdAsync(TKey id, bool tracking = true);
		DbSet<T> Table { get; }
	}
}
