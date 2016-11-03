using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Conventions
{
	public sealed class StringLengthConvention : IConvention
	{
		internal const string MaxLengthAnnotation = "MaxLength";
		internal const int DefaultStringLength = 50;

		public static readonly IConvention Instance = new StringLengthConvention();

		public void Apply(ModelBuilder modelBuilder)
		{
			foreach (var entity in modelBuilder.Model.GetEntityTypes())
			{
				foreach (var property in entity.GetProperties().Where(p => p.ClrType == typeof(string)))
				{
					var maxLength = property.FindAnnotation(MaxLengthAnnotation);

					if (maxLength == null)
					{
						property.AddAnnotation(MaxLengthAnnotation, DefaultStringLength);
					}
				}
			}
		}
	}
}
