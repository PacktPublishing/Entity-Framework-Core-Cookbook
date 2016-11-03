using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic
{
	[BlogValidation]
	public class Blog : IValidatableObject, IGroupValidatable
	{
		public int BlogId { get; set; }
		[CustomValidation(typeof(ForbiddenWordsValidator), "IsValid")]
		[Required]
		public string Name { get; set; }
		[PastDate]
		public DateTime CreationDate { get; set; }
		[MaxLength(50)]
		public string Url { get; set; }

		public ICollection<Post> Posts { get; set; } = new HashSet<Post>();

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			yield return ForbiddenWordsValidator.IsValid(Name);
		}
	}
}
