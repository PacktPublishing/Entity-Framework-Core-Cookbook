using Microsoft.Extensions.Configuration;

namespace UnitTests
{
	public abstract class BaseTests
	{
		protected BaseTests()
		{
			var builder = new ConfigurationBuilder()
				.AddJsonFile("appsettings.json");
			Configuration = builder.Build();
		}
		protected IConfiguration Configuration { get; private set; }
	}
}
