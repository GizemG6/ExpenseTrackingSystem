using ExpenseTrackingSystem.Application.Repositories;
using ExpenseTrackingSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Persistence.Repositories
{
	public class ReadRepository<T, TKey> : IReadRepository<T, TKey> where T : class
	{
		private readonly AppDbContext _context;

		public ReadRepository(AppDbContext context)
		{
			_context = context;
		}

		public DbSet<T> Table => _context.Set<T>();

		public async Task<List<T>> GetAllAsync(bool tracking = true)
		{
			var query = Table.AsQueryable();
			if (!tracking)
				query = query.AsNoTracking();

			return await query.ToListAsync();
		}

		public async Task<T> GetByIdAsync(TKey id, bool tracking = true)
		{
			var entity = await Table.FindAsync(id);

			if (entity == null)
				return null;

			if (!tracking)
				_context.Entry(entity).State = EntityState.Detached;

			return entity;
		}

		public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
		{
			var query = Table.AsQueryable();
			if (!tracking)
				query = query.AsNoTracking();

			return await query.FirstOrDefaultAsync(method);
		}

		public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
		{
			var query = Table.Where(method);
			if (!tracking)
				query = query.AsNoTracking();

			return query;
		}
	}
}
