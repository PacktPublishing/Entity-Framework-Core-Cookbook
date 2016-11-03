using BusinessLogic;
using System;
using System.Linq;

namespace UnitTests
{
	public static class BlogQueries
	{
		public static IQueryable<Blog> FilterByName(this IQueryable<Blog> blogs, string name)
		{
			return blogs.Where(x => x.Name.Contains(name));
		}

		public static IQueryable<Blog> BlogsCreatedInTheLastWeek(this IQueryable<Blog> blogs)
		{
			return blogs.Where(x => x.CreationDate >= DateTime.Today.AddDays(-7));
		}
	}
}
