using System.Collections.Generic;

namespace DataAccess
{
	public interface IQueryExecutor
	{
		IEnumerable<T> Execute<T>(QueryObject<T> query);
	}
}
