using System;
using System.Linq;
using BusinessLogic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
	public class BlogRepository : IBlogRepository
	{
		private readonly IDbContext _context;

		public BlogRepository(IDbContext context)
		{
			_context = context;
		}

		public void RollbackChanges()
		{
			_context.Rollback();
		}

		public void SaveChanges()
		{
			_context.SaveChanges();
		}

		public IQueryable<Blog> Set()
		{
			return _context.Set<Blog>();
		}
	}
}
