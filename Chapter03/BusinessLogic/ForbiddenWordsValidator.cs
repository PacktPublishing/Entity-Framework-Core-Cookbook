using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic
{
	public static class ForbiddenWordsValidator
	{
		public static ValidationResult IsValid(string word)
		{
			//TODO: check if the word is valid, maybe using a dictionary
			//for now, let’s assume it isn’t valid

			return new ValidationResult("Bad word detected");

			//if the word is ok, just return success
			//return ValidationResult.Success;
		}
	}
}
