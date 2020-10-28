using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Address_ model
  /// </summary>
  public class AddressModel : IValidatableObject
  {
    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public string City { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public string Country { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public LocationModel Location { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public string PostalCode { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public string StateProvince { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public string Street { get; set; }

    /// <summary>
    /// Represents the _Address_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (string.IsNullOrEmpty(City))
      {
        yield return new ValidationResult("City cannot be null.");
      }
      if (string.IsNullOrEmpty(Country))
      {
        yield return new ValidationResult("Country cannot be null.");
      }
      else if (Country != "USA" && Country != "US")
      {
        yield return new ValidationResult("Address must be in the United States");
      }
      if (string.IsNullOrEmpty(PostalCode))
      {
        yield return new ValidationResult("PostalCode cannot be null.");
      }
      if (string.IsNullOrEmpty(StateProvince))
      {
        yield return new ValidationResult("StateProvince cannot be null.");
      }
      if (string.IsNullOrEmpty(Street))
      {
        yield return new ValidationResult("Street cannot be null.");
      }
    }
  }
}
