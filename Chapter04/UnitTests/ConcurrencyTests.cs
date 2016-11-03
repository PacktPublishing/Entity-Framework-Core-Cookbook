using Xunit;
using DataAccess;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace UnitTests
{
	public class ConcurrencyTests : BaseTests
	{
		[Fact]
		public void CanUseOptimisticConcurrency()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				var blog = ctx.Blogs.First();
				var con = ctx.Database.GetDbConnection();
				var cmd = con.CreateCommand();
				cmd.CommandText = "UPDATE Blogs SET Name = Name + '_modified_'";
				blog.Name = "something to trigger a change";

				//Act
				cmd.ExecuteNonQuery();

				//Assert
				try
				{
					ctx.SaveChanges();
				}
				catch (Exception ex)
				{
					Assert.True(ex is
					DbUpdateConcurrencyException);
				}
			}
		}

		[Fact]
		public void CanUseSqlServerOptimisticConcurrency()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				var blog = ctx.Blogs.First();
				var con = ctx.Database.GetDbConnection();
				var cmd = con.CreateCommand();
				cmd.CommandText = "UPDATE Blogs SET Name = Name + '_modified_'";
				blog.Name = "something to trigger a change";

				//Act
				cmd.ExecuteNonQuery();

				//Assert
				try
				{
					ctx.SaveChanges();
				}
				catch (Exception ex)
				{
					Assert.True(ex is DbUpdateConcurrencyException);
				}
			}
		}
	}
}
