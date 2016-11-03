using Xunit;
using DataAccess;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace UnitTests
{
	public class DynamicLinqTests : BaseTests
	{
		[Fact]
		public void CanOrderByString()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var blogs = ctx.Blogs.OrderBy("CreationDate").ToList();
				//Assert
				Assert.NotEmpty(blogs);
			}
		}

		[Fact]
		public void CanFilterByString()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var blogs = ctx.Blogs.Where("CreationDate >= @0", DateTime.Today.AddDays(-7)).ToList();
				//Assert
				Assert.NotEmpty(blogs);
			}
		}

		[Fact]
		public void CanProjectByString()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var blogs = ctx.Blogs.Select<string>("Name.ToUpper()").ToList();
				//Assert
				Assert.NotEmpty(blogs);
			}
		}

		[Fact]
		public void CanDoComplexQueriesByString()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var blogs = ctx.Blogs.Where("it.CreationDate = @0 && it.Url.Contains(www)", DateTime.Today).ToList();
				//Assert
				Assert.NotEmpty(blogs);
			}
		}
	}
}
