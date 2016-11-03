using System.Linq;
using BusinessLogic;
using Moq;
using Xunit;
using DataAccess;

namespace UnitTests
{
    public class MockTest : BaseTest
    {
        [Fact]
        public void CanMock()
        {
            //Arrange
            var data = new[] { new Blog { Id = 1, Title = "Title" }, new
            Blog { Id = 2, Title = "No Title" } }.AsQueryable();
            var mock = new Mock<IDbContext>();
            mock.Setup(x => x.Set<Blog>()).Returns(data);
            //Act
            var context = mock.Object;
            var blogs = context.Set<Blog>();
            //Assert
            Assert.Equal(data, blogs);
        }
    }
}