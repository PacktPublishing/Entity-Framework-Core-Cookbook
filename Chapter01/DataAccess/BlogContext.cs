using Microsoft.EntityFrameworkCore;
using BusinessLogic;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;

namespace DataAccess
{
	public class BlogContext : DbContext, IDbContext
	{
		private readonly string _connectionString;

		public BlogContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		public DbSet<Blog> Blogs { get; set; }

		IQueryable<T> IDbContext.Set<T>()
		{
			return base.Set<T>();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_connectionString);
			base.OnConfiguring(optionsBuilder);
		}

		public object[] GetEntityKey<T>(T entity) where T : class
		{
			var state = Entry(entity);
			var metadata = state.Metadata;
			var key = metadata.FindPrimaryKey();
			var props = key.Properties.ToArray();
			return props.Select(x =>
			x.GetGetter().GetClrValue(entity)).ToArray();
		}

		public void Rollback()
		{
			ChangeTracker.Entries().ToList().ForEach(x =>
			{
				x.State = EntityState.Detached;
			});
		}
	}
}
