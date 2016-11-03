using Xunit;
using BusinessLogic;
using DataAccess;
using System.Linq;

namespace UnitTests
{
	public class CacheTests : BaseTests
	{
		[Fact]
		public void CanRetrieveFromCache()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var blogs = ctx.Blogs.ToList();

				//Assert
				var cachedBlogs = ctx.ChangeTracker.Entries<Blog>().Select(e => e.Entity);

				Assert.NotEmpty(cachedBlogs);
			}
		}
	}
}
