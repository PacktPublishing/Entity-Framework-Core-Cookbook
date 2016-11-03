using BusinessLogic;
using DataAccess;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Xunit;

namespace UnitTests
{
	public class ValidationTests : BaseTests
	{
		[Fact]
		public void CanInsertBlog()
		{
			using (var ctx = new BlogContext(
			Configuration["Data:Blog:ConnectionString"]))
			{
				//Arrange
				var blog = new Blog()
				{
					CreationDate = DateTime.UtcNow,
					Name = "A Blog",
					Url = "http://Some.url"
				};

				//Act
				ctx.Set<Blog>().Add(blog);

				//Assert
				Assert.True(ctx.SaveChanges() == 1);
			}
		}

		[Fact]
		public void CanDeleteBlog()
		{
			using (var ctx = new BlogContext(
			Configuration["Data:Blog:ConnectionString"]))
			{
				//Arrange
				var blog = new Blog()
				{
					CreationDate = DateTime.UtcNow,
					Name = "A Blog",
					Url = "http://Some.url"
				};

				//Act
				ctx.Set<Blog>().Add(blog);
				ctx.SaveChanges();

				ctx.Set<Blog>().Remove(blog);

				//Assert
				Assert.True(ctx.SaveChanges() == 1);
			}
		}

		[Fact]
		public void CanUpdateBlog()
		{
			using (var ctx = new BlogContext(
			Configuration["Data:Blog:ConnectionString"]))
			{
				//Arrange
				var blog = new Blog()
				{
					CreationDate = DateTime.UtcNow,
					Name = "A Blog",
					Url = "http://Some.url"
				};

				//Act
				ctx.Set<Blog>().Add(blog);
				ctx.SaveChanges();

				blog.Name += " - Updated";

				//Assert
				Assert.True(ctx.SaveChanges() == 1);
			}
		}


		[Fact]
		public void ShouldErrorOnNameTooLong()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				var builder = new StringBuilder();

				for (var i = 0; i < 20; i++)
				{
					builder.Append("Repeat this");
				}

				var blog = new Blog()
				{
					CreationDate = DateTime.UtcNow,
					Name = builder.ToString(),
					Url = "http://Some.url"
				};

				//Act
				ctx.Set<Blog>().Add(blog);

				//Assert
				Assert.ThrowsAny<ValidationException>(() =>
				ctx.SaveChanges());
			}
		}


		[Fact]
		public void ShouldErrorOnUrlRequired()
		{
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				//Arrange
				var blog = new Blog()
				{
					CreationDate = DateTime.UtcNow,
					Name = "A Blog"
				};

				//Act
				ctx.Set<Blog>().Add(blog);

				//Assert
				Assert.ThrowsAny<ValidationException>(() =>
				ctx.SaveChanges());
			}
		}

		[Fact]
		public void CanPreventChanges()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				var blog = new Blog { Name = "A Blog Name", CreationDate = DateTime.UtcNow };

				//Act
				ctx.Blogs.Add(blog);
				ctx.SaveChanges();

				blog.CreationDate = DateTime.UtcNow.AddDays(1);

				//Assert
				Assert.True(ctx.SaveChanges() == 0);
			}
		}


		[Fact]
		public void CanValidateDuplicates()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				var blog1 = new Blog { Name = "A Blog Name" };
				var blog2 = new Blog { Name = "A Blog Name" };

				//Assert
				Assert.ThrowsAny<ValidationException>(() =>
				{
					ctx.Blogs.Add(blog1);
					ctx.Blogs.Add(blog2);

					//Act
					ctx.SaveChanges();
				});
			}
		}

		[Fact]
		public void CanValidateAll()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				var blog = new Blog { Name = "A Bad Word" };

				//Assert
				Assert.ThrowsAny<ValidationException>(() =>
				{
					ctx.Blogs.Add(blog);

					//Act
					ctx.SaveChanges();
				});
			}
		}

		[Fact]
		public void CanValidateAttributes()
		{
			//Arrange
			using (var ctx = new BlogContext(Configuration["Data:Blog:ConnectionString"]))
			{
				var blog = new Blog { CreationDate = DateTime.Now.AddDays(1) };

				//Assert
				Assert.ThrowsAny<ValidationException>(() =>
				{
					ctx.Blogs.Add(blog);

					//Act
					ctx.SaveChanges();
				});
			}
		}
	}
}
