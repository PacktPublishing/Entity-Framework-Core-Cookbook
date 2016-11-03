using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
	public class UnitOfWork : IUnitOfWork
	{
		public IDbContext Context { get; private set; }

		public UnitOfWork(IDbContext context)
		{
			Context = context;
		}

		public void RegisterNew<T>(T entity) where T : class
		{
			Context.Entry(entity).State = EntityState.Added;
		}

		public void RegisterUnchanged<T>(T entity) where T : class
		{
			Context.Entry(entity).State = EntityState.Unchanged;
		}

		public void RegisterChanged<T>(T entity) where T : class
		{
			Context.Entry(entity).State = EntityState.Modified;
		}

		public void RegisterDeleted<T>(T entity) where T : class
		{
			Context.Entry(entity).State = EntityState.Deleted;
		}

		public void Refresh()
		{
			Context.Rollback();
		}

		public void Commit()
		{
			Context.SaveChanges();
		}
	}
}
