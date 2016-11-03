using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BusinessLogic;

namespace DataAccess
{
	public class BlogsQuery : QueryObject<Blog>
	{
		public DateTime? LowerDate { get; set; }
		public DateTime? HigherDate { get; set; }
		public string Name { get; set; }

		private readonly DbContext _context;

		public BlogsQuery(DbContext context)
		{
			_context = context;
		}

		public override IQueryable<Blog> ToQuery()
		{
			var query = _context.Set<Blog>().AsQueryable();
			if (LowerDate != null)
			{
				query = query.Where(b => b.CreationDate >= LowerDate);
			}
			if (HigherDate != null)
			{
				query = query.Where(b => b.CreationDate <= HigherDate);
			}
			if (!string.IsNullOrWhiteSpace(Name))
			{
				query = query.Where(b => b.Name.Contains(Name));
			}
			if (MaxItems != 0)
			{
				query = query.Take(MaxItems);
			}
			if (FirstItemIndex > 0)
			{
				query = query.Skip(FirstItemIndex);
			}
			return query;
		}
	}
}
