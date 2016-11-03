namespace DataAccess
{
	public interface IUnitOfWork
	{
		void RegisterNew<T>(T entity) where T : class;
		void RegisterUnchanged<T>(T entity) where T : class;
		void RegisterChanged<T>(T entity) where T : class;
		void RegisterDeleted<T>(T entity) where T : class;
		void Refresh();
		void Commit();
		IDbContext Context { get; }
	}
}
