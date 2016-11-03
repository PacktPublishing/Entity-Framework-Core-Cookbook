using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess
{
	public class SeparateDatabaseConfiguration : MultitenantConfiguration
	{
		private readonly IConfiguration _configuration;
		private readonly string _connectionStringTemplate;
		private readonly Func<IMultitenantAccessor, string>	_connectionStringProvider;

		public SeparateDatabaseConfiguration(IMultitenantAccessor accessor, IConfiguration configuration, string connectionStringTemplate) : base(accessor)
		{
			_configuration = configuration;
			_connectionStringTemplate = connectionStringTemplate ??	"Data:{0}:ConnectionString";
		}

		public SeparateDatabaseConfiguration(IMultitenantAccessor accessor,	Func<IMultitenantAccessor, string> csProvider) : base(accessor)
		{
			_connectionStringProvider = csProvider;
		}

		private string GetConnectionString()
		{
			var connectionString = string.Empty;
			if (_configuration != null)
			{
				var tenantId = Accessor.GetCurrentTenantId();
				var template = string.Format(_connectionStringTemplate, tenantId);
				connectionString = _configuration[template];
			}
			else if (_connectionStringProvider != null)
			{
				connectionString = _connectionStringProvider(Accessor);
			}
			return connectionString;
		}

		public override void Use(DbContextOptionsBuilder optionsBuilder)
		{
			var connectionString = GetConnectionString();
			optionsBuilder.UseSqlServer(connectionString);
		}

		public override void Use(ModelBuilder modelBuilder)
		{
		}
	}
}
