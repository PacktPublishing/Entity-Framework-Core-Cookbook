using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace DataAccess
{
	public interface IRepository<T> where T : class
	{
		IQueryable<T> Set();
		void RollbackChanges();
		void SaveChanges();
	}
}
