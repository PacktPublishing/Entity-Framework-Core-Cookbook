using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
	public abstract class MultitenantConfiguration
	{
		public static MultitenantConfiguration Provider	{ get; set; }
		protected MultitenantConfiguration(IMultitenantAccessor accessor)
		{
			Accessor = accessor;
		}
		public IMultitenantAccessor Accessor { get; private set; }
		public abstract void Use(DbContextOptionsBuilder optionsBuilder);
		public abstract void Use(ModelBuilder modelBuilder);
	}
}
