using Xunit;
using BusinessLogic;
using DataAccess;
using System;
using System.Linq;

namespace UnitTests
{
	public class FilterTests : BaseTests
	{
		[Fact]
		public void CanRetrieveFiltered()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var javaBlogs = ctx.JavaBlogs.ToList();

				//Assert
				Assert.NotEmpty(javaBlogs);
				Assert.All(javaBlogs, blog =>
				Assert.Contains("Java", blog.Name));
			}
		}

		[Fact]
		public void CanPreventInsertion()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Act
				var blog = new Blog
				{
					Name = "A Blog",
					CreationDate = DateTime.Today,
					Url = "http://a.url"
				};

				//Assert
				Assert.Throws<ArgumentException>(() =>
						ctx.JavaBlogs.Add(blog));
			}
		}
	}
}
