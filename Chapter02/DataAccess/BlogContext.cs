using Microsoft.EntityFrameworkCore;
using BusinessLogic;
using System;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore.Metadata;
using DataAccess.Conventions;
using System.Collections.Generic;

namespace DataAccess
{
	public class BlogContext : DbContext
	{
		private readonly string _connectionString;

		public BlogContext(string connectionString)
		{
			_connectionString = connectionString;
		}

		public Func<string> GetCurrentUser { get; set; } = () => WindowsIdentity.GetCurrent().Name;
		public Func<DateTime> GetCurrentTimestamp { get; set; } = () => DateTime.UtcNow;

		public DbSet<Blog> Blogs { get; set; }

		public override int SaveChanges()
		{
			foreach (var entry in ChangeTracker.Entries().Where(e => (e.Entity is IAuditable) && (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
			{
				entry.Property(Auditable.UpdatedBy).CurrentValue = GetCurrentUser();
				entry.Property(Auditable.UpdatedOn).CurrentValue = GetCurrentTimestamp();

				if (entry.State == EntityState.Added)
				{
					entry.Property(Auditable.CreatedBy).CurrentValue = GetCurrentUser();
					entry.Property(Auditable.CreatedOn).CurrentValue = GetCurrentTimestamp();
				}
			}

			return base.SaveChanges();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlServer(_connectionString);

			base.OnConfiguring(optionsBuilder);
		}

		public ISet<IConvention> Conventions { get; private set; } = new HashSet<IConvention>();

		protected void ApplyConventions(ModelBuilder modelBuilder)
		{
			foreach (var convention in Conventions)
			{
				convention.Apply(modelBuilder);
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ForSqlServerUseSequenceHiLo();

			ApplyConventions(modelBuilder);

			modelBuilder
				.Entity<BlogContent>()
				.HasKey(p => p.Id);

			modelBuilder
				.Entity<BlogContent>()
				.HasOne(p => p.Blog)
				.WithMany(b => b.Contents)
				.IsRequired();

			modelBuilder
				.Entity<FileContent>()
				.HasBaseType<BlogContent>();

			modelBuilder
				.Entity<PostContent>()
				.HasBaseType<BlogContent>();



			modelBuilder
				.Entity<Tag>();

			modelBuilder
				.Entity<PostTag>()
				.HasKey(x => new { x.PostId, x.TagId });

			modelBuilder
				.Entity<Post>()
				.HasMany(p => p.Tags)
				.WithOne(t => t.Post)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			modelBuilder
				.Entity<Tag>()
				.HasMany(t => t.Tags)
				.WithOne(p => p.Tag)
				.OnDelete(DeleteBehavior.Cascade)
				.IsRequired();

			modelBuilder
				.Entity<Post>()
				.HasOne(p => p.Blog)
				.WithMany(b => b.Posts)
				.IsRequired();


			modelBuilder
				.Entity<BlogDetail>()
				.HasKey(b => b.BlogId);

			modelBuilder.Entity<BlogDetail>()
				.HasOne(b => b.Blog)
				.WithOne(b => b.Detail)
				.IsRequired();

			modelBuilder
				.Entity<Blog>()
				.HasOne(b => b.Detail)
				.WithOne(d => d.Blog).OnDelete(DeleteBehavior.Cascade);

			modelBuilder
				.Entity<Blog>()
				.Property(typeof(DateTime), "Timestamp")
				.IsRequired();

			foreach (var entityType in modelBuilder.Model.GetEntityTypes())
			{
				if (typeof(IAuditable).IsAssignableFrom(entityType.ClrType))
				{
					modelBuilder.Entity(entityType.ClrType).Property(typeof(string), Auditable.CreatedBy).HasMaxLength(50).IsRequired();
					modelBuilder.Entity(entityType.ClrType).Property(typeof(string), Auditable.UpdatedBy).HasMaxLength(50).IsRequired();
					modelBuilder.Entity(entityType.ClrType).Property(typeof(DateTime), Auditable.CreatedOn).IsRequired();
					modelBuilder.Entity(entityType.ClrType).Property(typeof(DateTime), Auditable.UpdatedOn).IsRequired();
				}
			}

			base.OnModelCreating(modelBuilder);
		}
	}
}
