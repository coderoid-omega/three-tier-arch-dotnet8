using System.ComponentModel.DataAnnotations;

namespace ThreeTierArch.UI.Validations
{
    public class UpperCaseValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if(value != null)
            {
                if (!string.IsNullOrEmpty(value.ToString()))
                {
                    var firstLetter = (value as string)[0];
                    if (char.IsUpper(firstLetter))
                    {
                        return ValidationResult.Success;
                    }
                }
            }
            return new ValidationResult(ErrorMessage ?? "First letter should be uppercase");
        }
    }
}
