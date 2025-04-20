using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTrackingSystem.Application.Repositories
{
    public interface IWriteRepository<T, Tkey> where T : class
	{
		Task<bool> AddAsync(T entity);
		Task<bool> AddRangeAsync(List<T> entities);
		Task<bool> RemoveAsync(Tkey id);
		Task<bool> RemoveRangeAsync(List<Tkey> ids);
		Task<bool> UpdateAsync(T entity);
		Task<bool> SaveChangesAsync();
	}
}
