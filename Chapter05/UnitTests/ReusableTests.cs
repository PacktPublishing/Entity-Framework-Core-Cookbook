using Xunit;
using DataAccess;
using System;
using System.Linq;

namespace UnitTests
{
	public class ReusableTests : BaseTests
	{
		[Fact]
		public void CanReuseQueries()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var blogs = ctx.Blogs.FilterByName(".NET").ToList();

				//Assert
				Assert.NotEmpty(blogs);
				Assert.All(blogs, blog => Assert.Contains(".NET", blog.Name));
			}
		}

		[Fact]
		public void CanComposeQueries()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var blogs = ctx.Blogs.BlogsCreatedInTheLastWeek().Where(b => b.Name.Contains(".NET")).ToList();

				//Assert
				Assert.NotEmpty(blogs);
				Assert.All(blogs, blog =>
				{
					Assert.Contains(".NET", blog.Name);
					Assert.True(blog.CreationDate >= DateTime.Today.AddDays(-7));
				});
			}
		}
	}
}
