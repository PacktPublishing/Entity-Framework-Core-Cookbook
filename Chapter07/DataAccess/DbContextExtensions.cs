using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
	public static class DbContextExtensions
	{
		public static IEnumerable<T> Local<T>(this DbContext ctx) where T : class
		{
			return ctx
			.ChangeTracker
			.Entries<T>()
			.Select(e => e.Entity);
		}
	}
}
