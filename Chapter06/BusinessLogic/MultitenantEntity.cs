namespace BusinessLogic
{
	public class MultitenantEntity : IMultitenant
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}
}
