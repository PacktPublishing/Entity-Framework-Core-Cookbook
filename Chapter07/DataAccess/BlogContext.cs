using Microsoft.EntityFrameworkCore;
using BusinessLogic;

namespace DataAccess
{
	public class BlogContext : DbContext
	{
		private readonly string _connectionString;
		public BlogContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		public DbSet<Blog> Blogs { get; set; }

		protected override void OnConfiguring(
			DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder
					.UseSqlServer(_connectionString);
			base.OnConfiguring(optionsBuilder);
		}
	}
}
