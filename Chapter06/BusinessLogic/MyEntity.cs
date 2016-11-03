using System;

namespace BusinessLogic
{
	public class MyEntity : ISoftDeletable
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTime Date { get; set; }
	}
}
