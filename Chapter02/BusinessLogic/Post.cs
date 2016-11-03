using System;
using System.Collections.Generic;

namespace BusinessLogic
{
	public class Post
	{
		public int PostId { get; set; }
		public string Title { get; set; }
		public DateTime Timestamp { get; set; }
		public string Body { get; set; }
		public Blog Blog { get; set; }
		public ICollection<PostTag> Tags { get; private set; } = new HashSet<PostTag>();
	}
}
