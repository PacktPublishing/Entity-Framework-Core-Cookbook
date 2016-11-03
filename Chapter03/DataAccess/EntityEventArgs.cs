using Microsoft.EntityFrameworkCore;
using System.ComponentModel;

namespace DataAccess
{
	public sealed class EntityEventArgs : CancelEventArgs
	{
		public EntityEventArgs(object entity, EntityState state)
		{
			Entity = entity;
			State = state;
		}

		public EntityState State { get; private set; }
		public object Entity { get; private set; }
	}
}
