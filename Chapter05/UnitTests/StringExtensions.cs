namespace UnitTests
{
	public static class StringExtensions
	{
		public static int ComputeHash(this string str)
		{
			var hash = 0;
			foreach (var ch in str)
			{
				hash += (int)ch;
			}
			return hash;
		}
	}
}
