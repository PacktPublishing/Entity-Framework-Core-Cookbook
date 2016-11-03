using Xunit;
using DataAccess;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UnitTests
{
	public class ShadowTests : BaseTests
	{
		[Fact]
		public void CanQueryShadowProperties()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var blogs = ctx.Blogs.Where(b => EF.Property<DateTime>(b, "CreationDate") >= DateTime.Today.AddDays(-7)).ToList();

				//Assert
				Assert.NotEmpty(blogs);
			}
		}
	}
}
