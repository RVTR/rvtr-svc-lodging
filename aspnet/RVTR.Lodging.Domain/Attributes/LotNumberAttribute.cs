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
        Regex rgx = new Regex(@"^\d+([a-zA-Z]+)?$");
        if (value == null)
        {
            return new ValidationResult("Lot number can't be null.");
        }
        string LotNumber = value.ToString();
        if (LotNumber.Length >10)
        {
            return new ValidationResult("Lot number must be 10 digits maximum.");
        }
        if (!rgx.IsMatch(LotNumber))
        {
          return new ValidationResult("Lot number must be either a number or a number plus a series of letters.");
        }
        return ValidationResult.Success;
    }
  }
}
