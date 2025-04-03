using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Validators
{
    public class DateCheckAttribute:ValidationAttribute
    {
        protected override ValidationResult?IsValid(object? value,ValidationContext validationContext)
        {
            var date = (DateTime?)value;
            if (date < DateTime.Now)
            {
                return new ValidationResult("The Date must be Greater than or equal to to day Date");
            }
            return ValidationResult.Success;
        }
    }
}
