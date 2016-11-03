using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using BusinessLogic;

namespace DataAccess
{
	public class PartitionedDataConfiguration :	MultitenantConfiguration
	{
		public const string MultitenantColumn = "TenantId";

		public PartitionedDataConfiguration(IMultitenantAccessor accessor) : base(accessor)
		{
		}

		public override void Use(DbContextOptionsBuilder optionsBuilder)
		{
		}

		public override void Use(ModelBuilder modelBuilder)
		{
			var tenantId = Accessor.GetCurrentTenantId();
			foreach (var entity in modelBuilder.Model.GetEntityTypes().Where(e => typeof(IMultitenant).IsAssignableFrom(e.ClrType)))
			{
				modelBuilder
					.Entity(entity.ClrType)
					.HasDiscriminator(MultitenantColumn, typeof(string))
					.HasValue(tenantId);
			}
		}
	}
}
