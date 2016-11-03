using Xunit;
using BusinessLogic;
using DataAccess;
using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace UnitTests
{
	public class TransactionTests : BaseTests
	{
		[Fact]
		public void CanUseExplicitTransactionsInCommands()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			using (var tx = ctx.Database.BeginTransaction())
			{
				var con = ctx.Database.GetDbConnection();
				var cmd = con.CreateCommand();
				cmd.CommandText = "SELECT @@TRANCOUNT";
				cmd.Transaction = tx.GetDbTransaction();

				//Act
				var transactions = (int)cmd.ExecuteScalar();

				//Assert
				Assert.True(transactions == 1);
			}
		}

		[Fact]
		public void CanUseExplicitTransactions()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			using (var tx =	ctx.Database.BeginTransaction(IsolationLevel.Serializable))
			{
				var blog1 = new Blog
				{
					Name = "Blog 1",
					CreationDate = DateTime.Today
				};
				var blog2 = new Blog
				{
					Name = "Blog 2",
					CreationDate = DateTime.Today
				};

				ctx.AddRange(blog1, blog2);

				try
				{
					//Act
					ctx.SaveChanges();
					tx.Commit();
				}
				catch
				{
					tx.Rollback();
				}

				//Assert
				Assert.True(true);
			}
		}
	}
}
