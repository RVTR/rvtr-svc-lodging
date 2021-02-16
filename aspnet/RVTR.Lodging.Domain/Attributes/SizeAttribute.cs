using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Domain.Attributes
{
  /// <summary>
  /// Custom Validation Attribute for RentalModel's Size property
  /// </summary>
  public class SizeAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Size can't be null.");
        }

        Regex rgx = new Regex(@"^\d+ ?([Ff]t|[Yy]ards|[Mm]eters|[Mm]) ?x ?\d+ ?([Ff]t|[Yy]ards|[Mm]eters|[Mm])$");
        if (!rgx.IsMatch(value.ToString()))
        {
          return new ValidationResult("Size must be in the form '10 [unit?] x 10 [unit?]'");
        }
        return ValidationResult.Success;
    }
  }
}
