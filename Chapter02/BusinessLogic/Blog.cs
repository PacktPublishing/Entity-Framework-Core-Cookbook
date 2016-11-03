using System;
using System.Collections.Generic;

namespace BusinessLogic
{
	public class Blog : IAuditable
	{
		public int Id { get; set; }
		public string Title { get; set; }
		private DateTime Timestamp { get; set; }
		public BlogDetail Detail { get; set; }
		public ICollection<Post> Posts { get; private set; } = new HashSet<Post>();
		public ICollection<BlogContent> Contents { get; private set; } = new HashSet<BlogContent>();
	}
}
