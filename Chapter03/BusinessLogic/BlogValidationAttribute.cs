using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public class BlogValidationAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (!(value is Blog))
			{
				return ValidationResult.Success;
			}

			var blog = (Blog)value;

			//TODO: check the blog for invalid values
			//for now, let’s assume something is wrong with the name
			return new ValidationResult("Invalid name", new[] { "Name" });

			//yield return ValidationResult.Success;
		}
	}
}
