using Xunit;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using BusinessLogic;
using DataAccess;

namespace UnitTests
{
	public class StateTests : BaseTests
	{
		[Fact]
		public void CanSetState()
		{
			//Arrange
			Blog blog = null;

			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				blog = ctx.Blogs.First();
			}

			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Assert
				Assert.Equal(EntityState.Detached,
				ctx.Entry(blog).State);

				ctx.Entry(blog).State = EntityState.Modified;

				//Act
				var changes = ctx.SaveChanges();

				//Assert
				Assert.True(changes == 1);
			}
		}
	}
}
