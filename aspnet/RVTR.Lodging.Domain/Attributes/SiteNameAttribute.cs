using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Domain.Attributes
{
  /// <summary>
  /// Custom Validation Attribute for RentalModel's SiteName property
  /// </summary>
  public class SiteNameAttribute : ValidationAttribute
  {
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {

        if (value == null)
        {
            return new ValidationResult("SiteName can't be null.");
        }
        if (value.ToString().Length >100)
        {
            return new ValidationResult("SiteName must be 100 characters maximum.");
        }
        return ValidationResult.Success;
    }
  }
}
