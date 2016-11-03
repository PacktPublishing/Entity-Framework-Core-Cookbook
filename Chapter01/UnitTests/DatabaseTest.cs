using DataAccess;
using Xunit;

namespace UnitTests
{
	public class DatabaseTest : BaseTest
	{
		[Fact]
		public void CanCreateDatabase()
		{
			//Arrange
			var connectionString = Configuration["Data:Blog:ConnectionString"];
			var context = new BlogContext(connectionString);
			//Act
			var created = context.Database.EnsureCreated();
			//Assert
			Assert.True(created);
		}
	}
}
