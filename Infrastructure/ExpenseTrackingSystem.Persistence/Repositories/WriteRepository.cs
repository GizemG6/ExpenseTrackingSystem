using ExpenseTrackingSystem.Application.Repositories;
using ExpenseTrackingSystem.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Persistence.Repositories
{
	public class WriteRepository<T, TKey> : IWriteRepository<T, TKey> where T : class
	{
		private readonly AppDbContext _context;

		public WriteRepository(AppDbContext context)
		{
			_context = context;
		}
		public DbSet<T> Table => _context.Set<T>();

		public async Task<bool> AddAsync(T entity)
		{
			await Table.AddAsync(entity);
			return await SaveChangesAsync();
		}

		public async Task<bool> AddRangeAsync(List<T> entities)
		{
			await Table.AddRangeAsync(entities);
			return await SaveChangesAsync();
		}

		public async Task<bool> RemoveAsync(TKey id)
		{
			var entity = await Table.FindAsync(id);
			if (entity == null)
				return false;

			Table.Remove(entity);
			return await SaveChangesAsync();
		}

		public async Task<bool> RemoveRangeAsync(List<TKey> ids)
		{
			var entities = await Table.Where(e => ids.Contains(EF.Property<TKey>(e, "Id"))).ToListAsync();
			if (entities.Any())
			{
				Table.RemoveRange(entities);
			}
			return await SaveChangesAsync();
		}

		public async Task<bool> UpdateAsync(T entity)
		{
			Table.Update(entity);
			return await SaveChangesAsync();
		}

		public async Task<bool> SaveChangesAsync()
		{
			return await _context.SaveChangesAsync() > 0;
		}
	}
}
