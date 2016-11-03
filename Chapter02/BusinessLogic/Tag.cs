using System.Collections.Generic;

namespace BusinessLogic
{
	public class Tag
	{
		public int TagId { get; set; }
		public string Name { get; set; }
		public ICollection<PostTag> Tags { get; private set; } = new HashSet<PostTag>();
	}
}
