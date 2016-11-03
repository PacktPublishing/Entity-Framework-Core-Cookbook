using Microsoft.EntityFrameworkCore;
using BusinessLogic;

namespace DataAccess
{
	public class MyContext : DbContext
	{
		private readonly string _connectionString;

		public MyContext(DbContextOptions options) : base(options)
		{
		}

		public MyContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		public DbSet<MyEntity> MyEntities { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServerLolita();
			optionsBuilder.UseSqlServer(_connectionString);
			base.OnConfiguring(optionsBuilder);
		}
	}
}
