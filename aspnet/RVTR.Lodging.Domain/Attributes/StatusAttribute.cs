using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Domain.Attributes
{
  /// <summary>
  /// Custom Validation Attribute for RentalModel's Status property
  /// </summary>
  public class StatusAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return new ValidationResult("Status can't be null.");
        }

        Regex rgx = new Regex(@"^([Bb]ooked|[Aa]vailable)$");
        if (!rgx.IsMatch(value.ToString()))
        {
          return new ValidationResult("Status must be either 'Booked' or 'Available'");
        }
        return ValidationResult.Success;
    }
  }
}
