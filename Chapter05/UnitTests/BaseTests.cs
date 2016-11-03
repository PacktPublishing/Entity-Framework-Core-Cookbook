
using Microsoft.Extensions.Configuration;

namespace UnitTests
{
	public abstract class BaseTests
	{
		protected BaseTests()
		{
			var configurationBuilder = new ConfigurationBuilder();
			configurationBuilder.AddJsonFile("appSettings.json");
			Configuration = configurationBuilder.Build();
		}
		protected IConfiguration Configuration { get; set; }
	}
}
