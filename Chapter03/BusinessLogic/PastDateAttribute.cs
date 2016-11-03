using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic
{
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public class PastDateAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (!(value is DateTime))
			{
				return ValidationResult.Success;
			}

			var date = (DateTime)value;
			var now = DateTime.UtcNow;

			if (date.Date > now.Date)
			{
				return new ValidationResult("Cannot insert a future date");
			}

			return ValidationResult.Success;
		}
	}
}
