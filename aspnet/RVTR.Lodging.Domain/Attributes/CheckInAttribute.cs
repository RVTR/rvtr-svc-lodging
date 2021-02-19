using System;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.Domain.Attributes
{
  /// <summary>
  /// Custom Attribute to validate the Checkin property.
  /// </summary>
  public class CheckInAttribute : ValidationAttribute
  {
    /// <summary>
    /// Checks whether the specified value is valid with respect to the current validation attribute.
    /// </summary>
    /// <param name="value">The value to validate.</param>
    /// <param name="validationContext">The context information about the validation operation.</param>
    /// <returns>An instance of the ValidationResult class.</returns>
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
      if(value != null)
      {
        return new ValidationResult("Check In date required.");
      }
      var now = DateTime.Now;
      var checkInDate = (DateTime) value;

      if(DateTime.Compare(now, checkInDate) >= 0)
      {
        return ValidationResult.Success;
      }

      return new ValidationResult("Check In date has not occurred.");
    }
  }
}
