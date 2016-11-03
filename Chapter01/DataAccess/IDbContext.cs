using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;

namespace DataAccess
{
	public interface IDbContext
	{
		IQueryable<T> Set<T>() where T : class;
		EntityEntry<T> Entry<T>(T entity) where T : class;
		int SaveChanges();
		void Rollback();

		ChangeTracker ChangeTracker { get; }
	}
}
