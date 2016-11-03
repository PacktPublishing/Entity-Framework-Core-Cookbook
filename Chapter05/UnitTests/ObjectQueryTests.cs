using Xunit;
using DataAccess;
using System;
using System.Linq;

namespace UnitTests
{
	public class ObjectQueryTests : BaseTests
	{
		[Fact]
		public void CanQueryUsingObject()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var query = new BlogsQuery(ctx);
				query.LowerDate = DateTime.Today.AddDays(-7);
				query.HigherDate = DateTime.Today.AddDays(-1);
				query.Name = ".NET";
				query.MaxItems = 3;
				var blogs = (ctx as IQueryExecutor).Execute(query);

				//Assert
				Assert.NotEmpty(blogs);
				Assert.True(blogs.Count() <= 3);
				Assert.All(blogs, blog =>
				{
					Assert.Contains(".NET", blog.Name);
					Assert.True(blog.CreationDate >= DateTime.Today.AddDays(-7));
					Assert.True(blog.CreationDate <= DateTime.Today.AddDays(-1));
				});
			}
		}
	}
}
