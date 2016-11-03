using System.Linq;
using BusinessLogic;
using DataAccess;
using Moq;
using Xunit;
namespace UnitTests
{
	public class RepositoryTest : BaseTest
	{
		[Fact]
		public void ShouldAllowGettingASetOfObjectsGenerically()
		{
			//Arrange
			var data = new[] { new Blog { Id = 1, Title = "Title" }, new
			Blog { Id = 2, Title = "No Title" } }.AsQueryable();
			var mock = new Mock<IDbContext>();
			mock.Setup(x => x.Set<Blog>()).Returns(data);
			var context = mock.Object;
			var repository = new BlogRepository(context);
			//Act
			var blogs = repository.Set();
			//Assert
			Assert.Equal(data, blogs);
		}
	}
}
