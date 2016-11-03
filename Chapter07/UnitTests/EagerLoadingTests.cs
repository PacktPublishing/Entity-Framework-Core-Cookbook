using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using DataAccess;

namespace UnitTests
{
	public class EagerLoadingTests : BaseTests
	{
		[Fact]
		public void CanEagerLoad()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var blogsAndPosts = ctx
					.Blogs
					.Include(b => b.Posts)
					.ToList();

				//Assert
				Assert.NotNull(blogsAndPosts);
				Assert.NotEmpty(blogsAndPosts);
				Assert.All(blogsAndPosts, b => Assert.NotNull(b.Posts));
			}
		}
	}
}
