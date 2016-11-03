using System.Linq;

namespace DataAccess
{
	public abstract class QueryObject<T>
	{
		public int MaxItems { get; set; }
		public int FirstItemIndex { get; set; }
		public abstract IQueryable<T> ToQuery();
	}
}
