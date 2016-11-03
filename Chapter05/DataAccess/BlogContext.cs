using Microsoft.EntityFrameworkCore;
using System.Linq;
using BusinessLogic;
using System;
using System.Collections.Generic;

namespace DataAccess
{
	public class BlogContext : DbContext, IQueryExecutor
	{
		private readonly string _connectionString;

		public BlogContext(string connectionString)
		{
			_connectionString = connectionString;
			this.JavaBlogs = FilteredDbSet<Blog>.Create<Blog>(this, x => x.Name.Contains("Java"));
			this.DotNetBlogs = FilteredDbSet<Blog>.Create<Blog>(this, x => x.Name.Contains(".NET"));
		}

		public DbSet<Blog> JavaBlogs { get; set; }
		public DbSet<Blog> DotNetBlogs { get; set; }
		public DbSet<Blog> Blogs { get; set; }

		public IEnumerable<T> Execute<T>(QueryObject<T> query)
		{
			return query
			.ToQuery()
			.ToList();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_connectionString);
			base.OnConfiguring(optionsBuilder);
		}

		public override int SaveChanges()
		{
			foreach (var entry in ChangeTracker.Entries<Blog>().Where(x => x.State == EntityState.Added))
			{
				entry.Property("CreationDate").CurrentValue = DateTime.UtcNow;
			}

			return base.SaveChanges();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Blog>()
				.Property(typeof(DateTime), "CreationDate")
				.IsRequired(true);

			base.OnModelCreating(modelBuilder);
		}

	}
}
