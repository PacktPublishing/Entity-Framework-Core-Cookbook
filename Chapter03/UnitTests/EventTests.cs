using Xunit;
using BusinessLogic;
using DataAccess;
using System;

namespace UnitTests
{
	public class EventTests : BaseTests
	{
		[Fact]
		public void CanPreventChanges()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				ctx.SavingChanges += (sender, e) =>
				{
					e.Cancel = true;
				};

				var blog = new Blog
				{
					Name = "A Blog Name",
					CreationDate = DateTime.UtcNow
				};

				//Act
				ctx.Blogs.Add(blog);

				//Assert
				Assert.True(ctx.SaveChanges() == 0);
			}
		}

		[Fact]
		public void CanSetValues()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				ctx.SavingChanges += (sender, e) =>
				{
					((Blog)e.Entity).CreationDate = DateTime.UtcNow;
				};

				var blog = new Blog
				{
					Name = "A Blog Name",
					Url = "http://some.blog"
				};

				//Act
				ctx.Blogs.Add(blog);

				//Assert
				Assert.True(ctx.SaveChanges() == 1);
			}
		}
	}
}
