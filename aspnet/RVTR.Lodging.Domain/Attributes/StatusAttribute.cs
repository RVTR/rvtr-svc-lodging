using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Domain.Attributes
{
  public class StatusAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var rentalModel = (RentalModel)validationContext.ObjectInstance;

        if (rentalModel.Status == null)
        {
            return new ValidationResult("Status can't be null.");
        }

        Regex rgx = new Regex(@"^([Bb]ooked|[Aa]vailable)$");
        if (!rgx.IsMatch(rentalModel.Status))
        {
          return new ValidationResult("Status must be either 'Booked' or 'Available'");
        }
        return ValidationResult.Success;
    }
  }
}
