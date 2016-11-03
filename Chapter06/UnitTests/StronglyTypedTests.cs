using BusinessLogic;
using DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Xunit;

namespace UnitTests
{
	public class StronglyTypedTests : BaseTests
	{
		[Fact]
		public void CanDelete()
		{
			//Arrange
			using (var ctx = new MyContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				ctx.MyEntities.Add(new MyEntity { Name = "test" });
				ctx.SaveChanges();
				var result = ctx.MyEntities.Where(b => b.Name == "test").Delete();

				//Assert
				Assert.True(result == 1);
			}
		}

		[Fact]
		public void CanUpdate()
		{
			//Arrange
			using (var ctx = new MyContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				ctx.MyEntities.Add(new MyEntity { Name = "test" });
				ctx.SaveChanges();
				var result = ctx.MyEntities.Where(b => b.Name == "test").SetField(b => b.Date).AddDays(1).Update();

				//Assert
				Assert.True(result == 1);
			}
		}
	}
}
