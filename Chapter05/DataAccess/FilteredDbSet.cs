using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DataAccess
{
	public class FilteredDbSet<TEntity> : InternalDbSet<TEntity> where TEntity : class
	{
		private readonly Expression<Func<TEntity, bool>> _filter;
		private readonly Func<TEntity, bool> _condition;

		private static DbSet<TEntity> GetDbSet(DbContext context)
		{
			return context.Set<TEntity>();
		}

		private static IQueryable<TEntity> GetSource(DbContext context,	Expression<Func<TEntity, bool>> filter)
		{
			var query = context.Set<TEntity>() as IQueryable<TEntity>;
			query = query.Where(filter);
			return query;
		}

		private void EnsureMatchesFilter(IEnumerable<TEntity> entities)
		{
			foreach (var entity in entities)
			{
				EnsureMatchesFilter(entity);
			}
		}

		private void EnsureMatchesFilter(TEntity entity)
		{
			if (!_condition(entity))
			{
				throw new ArgumentException("Entity does not match the filter");
			}
		}

		protected FilteredDbSet(DbContext context, Expression<Func<TEntity, bool>> filter) : base(GetSource(context, filter), GetDbSet(context))
		{
			_filter = filter;
			_condition = _filter.Compile();
		}

		public static DbSet<T> Create<T>(DbContext context, Expression<Func<T, bool>> filter) where T : class
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter");
			}

			return new FilteredDbSet<T>(context, filter);
		}

		public override EntityEntry<TEntity> Add(TEntity entity)
		{
			EnsureMatchesFilter(entity);
			return base.Add(entity);
		}

		public override void AddRange(IEnumerable<TEntity> entities)
		{
			EnsureMatchesFilter(entities);
			base.AddRange(entities);
		}

		public override void AddRange(params TEntity[] entities)
		{
			EnsureMatchesFilter(entities);
			base.AddRange(entities);
		}

		public override EntityEntry<TEntity> Attach(TEntity entity)
		{
			EnsureMatchesFilter(entity);
			return base.Attach(entity);
		}

		public override void AttachRange(IEnumerable<TEntity> entities)
		{
			EnsureMatchesFilter(entities);
			base.AttachRange(entities);
		}

		public override void AttachRange(params TEntity[] entities)
		{
			EnsureMatchesFilter(entities);
			base.AttachRange(entities);
		}

		public override EntityEntry<TEntity> Update(TEntity entity)
		{
			EnsureMatchesFilter(entity);
			return base.Update(entity);
		}

		public override void UpdateRange(IEnumerable<TEntity> entities)
		{
			EnsureMatchesFilter(entities);
			base.UpdateRange(entities);
		}

		public override void UpdateRange(params TEntity[] entities)
		{
			EnsureMatchesFilter(entities);
			base.UpdateRange(entities);
		}
	}
}
