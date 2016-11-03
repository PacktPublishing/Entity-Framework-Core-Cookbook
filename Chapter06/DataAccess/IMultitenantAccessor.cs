namespace DataAccess
{
	public interface IMultitenantAccessor
	{
		string GetCurrentTenantId();
	}
}
