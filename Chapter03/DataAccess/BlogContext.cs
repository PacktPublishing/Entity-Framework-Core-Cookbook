using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using BusinessLogic;
using System.Data;

namespace DataAccess
{
	public class BlogContext : DbContext
	{
		private readonly string _connectionString;

		public BlogContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		public DbSet<Blog> Blogs { get; set; }

		public event EventHandler<EntityEventArgs> SavingChanges;

		protected void OnSavingChanges(EntityEventArgs e)
		{
			var handler = SavingChanges;

			if (handler != null)
			{
				handler(this, e);
			}

			if (e.Cancel == true)
			{
				if (e.State == EntityState.Added)
				{
					Entry(e.Entity).State =	EntityState.Detached;
				}
				else
				{
					Entry(e.Entity).State =	EntityState.Unchanged;
				}
			}
		}


		protected void ValidateEntries(Type type, IEnumerable<object> entities, EntityState state)
		{
			if (type == typeof(Blog))
			{
				var count = entities.Count();

				var countDistinctNames = entities
					.OfType<Blog>()
					.Select(b => b.Name.ToLowerInvariant())
					.Distinct()
					.Count();

				if (count != countDistinctNames)
				{
					throw new ValidationException("Duplicate blog names detected");
				}
			}
		}

		protected void ValidateDirtyEntries()
		{
			var serviceProvider = this.GetService<IServiceProvider>();
			var items = new Dictionary<object, object>();

			var addedEntries = ChangeTracker
				.Entries()
				.Where(e => (e.Entity is IGroupValidatable) && (e.State == EntityState.Added))
				.Select(e => e.Entity)
				.GroupBy(e => e.GetType())
				.Select(g => new { Type = g.Key, Entities = g.ToList() });

			var modifiedEntries = ChangeTracker
				.Entries()
				.Where(e => (e.Entity is IGroupValidatable) && (e.State == EntityState.Modified))
				.Select(e => e.Entity)
				.GroupBy(e => e.GetType())
				.Select(g => new { Type = g.Key, Entities = g.ToList() });

			foreach (var g in addedEntries)
			{
				ValidateEntries(g.Type, g.Entities,
				EntityState.Added);
			}

			foreach (var g in modifiedEntries)
			{
				ValidateEntries(g.Type, g.Entities,
				EntityState.Modified);
			}

			foreach (var entry in ChangeTracker.Entries().Where(e => (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
			{
				var entity = entry.Entity;
				var context = new ValidationContext(entity, serviceProvider, items);
				var results = new List<ValidationResult>();

				if (Validator.TryValidateObject(entity, context, results, true) == false)
				{
					foreach (var result in results)
					{
						if (result != ValidationResult.Success)
						{
							throw new ValidationException(result.ErrorMessage);
						}
					}
				}
			}
		}

		protected bool ValidateProperty(object entity, string propertyName, object originalValue, object currentValue)
		{
			if (entity is Blog)
			{
				if (propertyName == "CreationDate")
				{
					var originalDate = (DateTime)originalValue;
					var currentDate = (DateTime)currentValue;

					if (currentDate > originalDate)
					{
						return false;
					}
				}
			}

			return true;
		}

		protected void ValidateModifiedProperties()
		{
			ChangeTracker.DetectChanges();

			foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Modified))
			{
				foreach (var propName in entry.Metadata.GetProperties().Select(p => p.Name))
				{
					var prop = entry.Property(propName);

					if (prop.IsModified == true)
					{
						if (ValidateProperty(entry.Entity,	propName, prop.OriginalValue, prop.CurrentValue) == false)
						{
							prop.CurrentValue =	prop.OriginalValue;
						}
					}
				}
			}

			ChangeTracker.DetectChanges();
		}


		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_connectionString);

			base.OnConfiguring(optionsBuilder);
		}

		private int SaveWithStoredProcedures()
		{
			var changes = 0;

			foreach (var e in ChangeTracker.Entries().Where(e => (e.State != EntityState.Unchanged) && (e.Entity is Blog)))
			{
				var blog = e.Entity as Blog;

				switch (e.State)
				{
					case EntityState.Added:
						var con = Database.GetDbConnection();

						using (var cmd = con.CreateCommand())
						{
							con.Open();

							var name = cmd.CreateParameter();
							name.ParameterName = "p0";
							name.Value = blog.Name;

							var url = cmd.CreateParameter();
							url.ParameterName = "p1";
							url.Value = blog.Url;

							var creationDate = cmd.CreateParameter();
							creationDate.ParameterName = "p2";
							creationDate.Value = blog.CreationDate;

							var blogid = cmd.CreateParameter();
							blogid.ParameterName = "blogid";
							blogid.DbType = DbType.Int32;
							blogid.Direction = ParameterDirection.Output;

							cmd.CommandText = "EXEC @blogid = dbo.InsertBlog @Name = @p0, @Url = @p1, @CreationDate = @p2";
							cmd.Parameters.AddRange(new[] { name, url, creationDate, blogid });

							cmd.ExecuteNonQuery();

							blog.BlogId = (int)blogid.Value;

							con.Close();
						}
						break;

					case EntityState.Modified:
						Database.ExecuteSqlCommand("EXEC dbo.UpdateBlog @BlogId = @p0, @Name = @p1, @Url = @p2, @CreationDate = @p3", blog.BlogId, blog.Name, blog.Url, blog.CreationDate);
						break;

					case EntityState.Deleted:
						Database.ExecuteSqlCommand("EXEC dbo.DeleteBlog @BlogId = @p0", blog.BlogId);
						break;
				}

				e.State = EntityState.Unchanged;
				++changes;
			}

			return changes + base.SaveChanges();
		}

		public override int SaveChanges()
		{
			this.SaveWithStoredProcedures();

			foreach (var entry in ChangeTracker.Entries().Where(e => e.State != EntityState.Unchanged))
			{
				var args = new EntityEventArgs(entry.Entity, entry.State);

				OnSavingChanges(args);
			}

			ValidateModifiedProperties();
			ValidateDirtyEntries();

			return base.SaveChanges();
		}
	}
}
