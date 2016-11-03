namespace BusinessLogic
{
	public class FileContent : BlogContent
	{
		public string ContentType { get; set; }
		public int? Size { get; set; }
		public byte[] Contents { get; set; }

	}
}
