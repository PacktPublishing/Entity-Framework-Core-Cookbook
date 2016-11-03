using System.Linq;
using Microsoft.AspNetCore.Http;

namespace DataAccess
{
	public class HostHeaderMultitenantAccessor : IMultitenantAccessor
	{
		private readonly IHttpContextAccessor _accessor;
		public HostHeaderMultitenantAccessor(IHttpContextAccessor accessor)
		{
			_accessor = accessor;
		}
		public string GetCurrentTenantId()
		{
			var context = _accessor.HttpContext;
			var parts = context.Request.Host.Host.Split('.');
			return parts.ElementAt(parts.Length - 2) + "." +
			parts.Last();
		}
	}
}
