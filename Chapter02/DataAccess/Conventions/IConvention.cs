using Microsoft.EntityFrameworkCore;

namespace DataAccess.Conventions
{
	public interface IConvention
	{
		void Apply(ModelBuilder modelBuilder);
	}
}
