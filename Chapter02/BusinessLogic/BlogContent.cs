using System;

namespace BusinessLogic
{
	public abstract class BlogContent
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime Timestamp { get; set; }
		public Blog Blog { get; set; }
	}
}
