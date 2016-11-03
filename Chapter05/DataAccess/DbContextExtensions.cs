using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System;

namespace DataAccess
{
	public static class DbContextExtensions
	{
		public static DbSet<T> FilteredSet<T>(this DbContext context, Expression<Func<T, bool>> filter) where T : class
		{
			return FilteredDbSet<T>.Create<T>(context, filter);
		}

		public static void Evict<T>(this DbContext ctx)	where T : class
		{
			foreach (var entry in ctx.ChangeTracker.Entries<T>().ToList())
			{
				ctx.Entry(entry.Entity).State =	EntityState.Detached;
			}
		}
	}

}
