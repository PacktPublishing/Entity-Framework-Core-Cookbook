using Microsoft.EntityFrameworkCore;
using BusinessLogic;
using System.Linq;
using System.Reflection;

namespace DataAccess
{
	public class SoftDeleteContext : DbContext
	{
		private readonly string _connectionString;

		public SoftDeleteContext(DbContextOptions options) : base(options)
		{
		}

		public SoftDeleteContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		public DbSet<MyEntity> MyEntities { get; set; }

		public override int SaveChanges()
		{
			foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted))
			{
				if (entry.Entity is ISoftDeletable)
				{
					entry
						.Property("IsDeleted")
						.CurrentValue = true;
					entry.State = EntityState.Modified;
				}
			}
			return base.SaveChanges();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (var entity in modelBuilder.Model.GetEntityTypes())
			{
				if (typeof(ISoftDeletable).IsAssignableFrom(entity.ClrType))
				{
					modelBuilder
						.Entity(entity.ClrType)
						.HasDiscriminator("IsDeleted", typeof(bool))
						.HasValue(false);

					modelBuilder
						.Entity(entity.ClrType)
						.Property(typeof(bool), "IsDeleted")
						.IsRequired(true)
						.HasDefaultValue(false);

					modelBuilder
						.Entity(entity.ClrType)
						.Property(typeof(bool), "IsDeleted")
						.Metadata
						.IsReadOnlyAfterSave = false;
				}
			}

			base.OnModelCreating(modelBuilder);
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_connectionString);
			base.OnConfiguring(optionsBuilder);
		}
	}
}
