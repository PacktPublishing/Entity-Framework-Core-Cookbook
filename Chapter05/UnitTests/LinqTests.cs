using Xunit;
using DataAccess;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
	public class LinqTests : BaseTests
	{
		[Fact]
		public void CanFilterAfterSql()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var blogs = ctx.Blogs.FromSql("EXEC dbo.GetBlogs").Where(b => b.Name.Contains("Development")).ToList();

				//Assert
				Assert.NotEmpty(blogs);
			}
		}

		[Fact]
		public void CanSelectAfterSql()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var blogNames = ctx.Blogs.FromSql("SELECT b.* FROM Blogs b").Select(b => b.Name).ToList();

				//Assert
				Assert.NotEmpty(blogNames);
			}
		}


		[Fact]
		public void CanFilterClientSide()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var singleHash = (from blog in ctx.Blogs
								  where blog.Name.ComputeHash() < 100
								  select blog.Name.ComputeHash()).First();

				//Assert
				Assert.True(singleHash < 100);
			}
		}

		[Fact]
		public void CanSelectClientSide()
		{
			//Arrange
			using (var ctx = new BlogContext(
			Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var singleHash = (from blog in ctx.Blogs
								  select blog.Name.ComputeHash()).First();

				//Assert
				Assert.True(singleHash > 0);
			}
		}
	}
}
