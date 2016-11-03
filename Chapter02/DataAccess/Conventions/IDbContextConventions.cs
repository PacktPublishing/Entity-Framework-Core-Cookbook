using System.Collections.Generic;

namespace DataAccess.Conventions
{
	public interface IDbContextConventions
	{
		ISet<IConvention> Conventions { get; }
	}
}
