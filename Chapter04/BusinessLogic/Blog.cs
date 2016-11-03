using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic
{
	public class Blog
	{
		public int BlogId { get; set; }
		[ConcurrencyCheck]
		public string Name { get; set; }
		[ConcurrencyCheck]
		public DateTime CreationDate { get; set; }
		public string Url { get; set; }
		[Timestamp]
		public byte[] RowVersion { get; private set; }

	}
}
