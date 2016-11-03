using System;

namespace BusinessLogic
{
	public class BlogDetail
	{
		public int BlogId { get; set; }
		public Blog Blog { get; set; }
		public DateTime CreatedOn { get; set; }
		public string Description { get; set; }
		public string Url { get; set; }
	}
}