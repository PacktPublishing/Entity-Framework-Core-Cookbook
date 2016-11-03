using Xunit;
using BusinessLogic;
using DataAccess;
using System;
using Microsoft.Framework.Configuration;

namespace UnitTests
{
	public class SoftDeleteTests : BaseTests
	{
		[Fact]
		public void CanSoftDelete()
		{
			//Arrange
			using (var ctx = new SoftDeleteContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var entity = new MyEntity { Name = "Test" };
				ctx.MyEntities.Add(entity);
				var inserts = ctx.SaveChanges();
				ctx.Entry(entity).State = EntityState.Detached;
				entity = ctx.MyEntities.First();
				ctx.MyEntities.Remove(entity);
				var deletes = ctx.SaveChanges();
				//Assert
				Assert.True(deletes == 1);
			}
		}
	}
}
