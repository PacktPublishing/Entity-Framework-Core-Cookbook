using System;
using System.Collections.Generic;

namespace BusinessLogic
{
	public class Blog
	{
		public Blog()
		{
			Posts = new HashSet<Post>();
		}
		public int BlogId { get; set; }
		public string Name { get; set; }
		public DateTime CreationDate { get; set; }
		public string Url { get; set; }
		public virtual ICollection<Post> Posts { get; set; }
	}
}
