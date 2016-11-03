using System;
using System.Linq;
using BusinessLogic;
using DataAccess;
using Moq;
using Xunit;

namespace UnitTests
{
	public class UnitOfWorkTest : BaseTest
	{
		[Fact]
		public void ShouldReadToDatabaseOnRead()
		{
			//Arrange
			var findCalled = false;
			var mock = new Mock<IDbContext>();
			mock.Setup(x => x.Set<Blog>()).Callback(() => findCalled =
			true);
			var context = mock.Object;
			var unitOfWork = new UnitOfWork(context);
			var repository = new BlogRepository(context);
			//Act
			var blogs = repository.Set();
			//Assert
			Assert.True(findCalled);
		}

		[Fact]
		public void ShouldNotCommitToDatabaseOnDataChange()
		{
			//Arrange
			var saveChangesCalled = false;
			var data = new[] { new Blog() { Id = 1, Title = "Test" }
			}.AsQueryable();
			var mock = new Mock<IDbContext>();
			mock.Setup(x => x.Set<Blog>()).Returns(data);
			mock.Setup(x => x.SaveChanges()).Callback(() =>
			saveChangesCalled = true);
			var context = mock.Object;
			var unitOfWork = new UnitOfWork(context);
			var repository = new BlogRepository(context);
			//Act
			var blogs = repository.Set();
			blogs.First().Title = "Not Going to be Written";
			//Assert
			Assert.False(saveChangesCalled);
		}

		[Fact]
		public void ShouldPullDatabaseValuesOnARollBack()
		{
			//Arrange
			var saveChangesCalled = false;
			var rollbackCalled = false;
			var data = new[] { new Blog() { Id = 1, Title = "Test" }
			}.AsQueryable();
			var mock = new Mock<IDbContext>();
			mock.Setup(x => x.Set<Blog>()).Returns(data);
			mock.Setup(x => x.SaveChanges()).Callback(() =>
			saveChangesCalled = true);
			mock.Setup(x => x.Rollback()).Callback(() => rollbackCalled =
			true);
			var context = mock.Object;
			var unitOfWork = new UnitOfWork(context);
			var repository = new BlogRepository(context);
			//Act
			var blogs = repository.Set();
			blogs.First().Title = "Not Going to be Written";
			repository.RollbackChanges();
			//Assert
			Assert.False(saveChangesCalled);
			Assert.True(rollbackCalled);
		}

		[Fact]
		public void ShouldCommitToDatabaseOnSaveCall()
		{
			//Arrange
			var saveChangesCalled = false;
			var data = new[] { new Blog() { Id = 1, Title = "Test" }
			}.AsQueryable();
			var mock = new Mock<IDbContext>();
			mock.Setup(x => x.Set<Blog>()).Returns(data);
			mock.Setup(x => x.SaveChanges()).Callback(() =>
			saveChangesCalled = true);
			var context = mock.Object;
			var unitOfWork = new UnitOfWork(context);
			var repository = new BlogRepository(context);
			//Act
			var blogs = repository.Set();
			blogs.First().Title = "Going to be Written";
			repository.SaveChanges();
			//Assert
			Assert.True(saveChangesCalled);
		}

		[Fact]
		public void ShouldNotCommitOnError()
		{
			//Arrange
			var rollbackCalled = false;
			var data = new[] { new Blog() { Id = 1, Title = "Test" }
			}.AsQueryable();
			var mock = new Mock<IDbContext>();
			mock.Setup(x => x.Set<Blog>()).Returns(data);
			mock.Setup(x => x.SaveChanges()).Throws(new Exception());
			mock.Setup(x => x.Rollback()).Callback(() => rollbackCalled = true);
			var context = mock.Object;
			var unitOfWork = new UnitOfWork(context);
			var repository = new BlogRepository(context);
			//Act
			var blogs = repository.Set();
			blogs.First().Title = "Not Going to be Written";

			try
			{
				repository.SaveChanges();
			}
			catch
			{
			}
			//Assert
			Assert.True(rollbackCalled);
		}
	}
}
