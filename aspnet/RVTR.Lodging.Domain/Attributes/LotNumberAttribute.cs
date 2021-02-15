using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Domain.Attributes
{
  /// <summary>
  /// Custom Validation Attribute for RentalModel's LotNumber property
  /// </summary>
  public class LotNumberAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var rentalModel = (RentalModel)validationContext.ObjectInstance;

        if (rentalModel.LotNumber == null)
        {
            return new ValidationResult("Lot number can't be null.");
        }
        if (rentalModel.LotNumber.Length >10)
        {
            return new ValidationResult("Lot number must be 10 digits maximum.");
        }
        Regex rgx = new Regex(@"^\d+([a-zA-Z]+)?$");
        if (!rgx.IsMatch(rentalModel.LotNumber))
        {
          return new ValidationResult("Lot number must be either a number or a number plus a series of letters.");
        }
        return ValidationResult.Success;
    }
  }
}
