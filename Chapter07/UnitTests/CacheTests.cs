using Xunit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Microsoft.Extensions.Caching.Memory;
using System.Threading;
using BusinessLogic;
using DataAccess;
using Z.EntityFramework.Plus;

namespace UnitTests
{
	public class CacheTests : BaseTests
	{
		[Fact]
		public void CanQueryFirstLevelCache()
		{
			//Arrange
			using (var ctx = new MyContext(Configuration["Data:My:ConnectionString"]))
			{
				//Act
				var entity = new MyEntity
				{
					Name = "Test",
					Date = DateTime.Today
				};
				ctx.MyEntities.Add(entity);
				ctx.SaveChanges();
				ctx.Entry(entity).State = EntityState.Detached;
				var entities = ctx
					.Local<MyEntity>()
					.ToList();
				//Assert
				Assert.NotNull(entities);
				Assert.Empty(entities);
				ctx.MyEntities.ToList();
				entities = ctx
					.Local<MyEntity>()
					.ToList();
				Assert.NotNull(entities);
				Assert.NotEmpty(entities);
			}
		}

		[Fact]
		public void CanQuerySecondLevelCache()
		{
			//Arrange
			var cache = QueryCacheManager.Cache as MemoryCache;
			var query = null as IQueryable<MyEntity>;
			var cacheKey = string.Empty;
			using (var ctx = new MyContext(Configuration["Data:My:ConnectionString"]))
			{
				//Act
				var entity = new MyEntity
				{
					Name = "Test",
					Date = DateTime.Today
				};
				ctx.MyEntities.Add(entity);
				ctx.SaveChanges();
				ctx.Entry(entity).State = EntityState.Detached;
				Assert.Equal(0, cache.Count);
				var entities = query
					.FromCache(new MemoryCacheEntryOptions
					{
						SlidingExpiration = TimeSpan.FromSeconds(5)
					});
				cacheKey = QueryCacheManager.GetCacheKey(query, new string[0]);
				var isFound = cache.TryGetValue(cacheKey, out entities);
				//Assert
				Assert.True(isFound);
				Assert.NotNull(cacheKey);
				Assert.NotNull(entities);
				Assert.NotEmpty(entities);
				Assert.Equal(1, cache.Count);
			}
			using (var ctx = new MyContext(Configuration["Data:My:ConnectionString"]))
			{
				var entities = query.FromCache();
				var isFound = cache.TryGetValue(cacheKey, out entities);
				//Assert
				Assert.True(isFound);
				Assert.NotNull(entities);
				Assert.NotEmpty(entities);
				Assert.Equal(1, cache.Count);
				//two minutes
				Thread.Sleep(2 * 60 * 1000);
				isFound = cache.TryGetValue(cacheKey, out entities);
				Assert.False(isFound);
			}
		}
	}
}
