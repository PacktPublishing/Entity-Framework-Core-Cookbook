using System.Linq;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using BusinessLogic;

namespace DataAccess
{
	public class SeparateSchemaConfiguration : MultitenantConfiguration
	{
		public SeparateSchemaConfiguration(IMultitenantAccessor accessor) : base(accessor)
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
					.ForSqlServerToTable(modelBuilder.Model.FindEntityType(entity.ClrType).SqlServer().TableName, tenantId);
			}
		}
	}
}
