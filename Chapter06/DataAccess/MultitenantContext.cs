using BusinessLogic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
	public class MultitenantContext : DbContext
	{
		private readonly string _connectionString;

		public MultitenantContext(DbContextOptions options) : base(options)
		{
		}

		public MultitenantContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		public DbSet<MultitenantEntity> MultitenantEntities	{ get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_connectionString);
			MultitenantConfiguration.Provider?.Use(optionsBuilder);
			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			MultitenantConfiguration.Provider?.Use(modelBuilder);
			base.OnModelCreating(modelBuilder);
		}
	}
}
