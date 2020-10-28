using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Location_ model
  /// </summary>
  public class LocationModel : IValidatableObject
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
    public int AddressId { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public AddressModel Address { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public string Latitude { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public string Longitude { get; set; }

    /// <summary>
    /// Represents the _Location_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Address == null)
      {
        yield return new ValidationResult("Address cannot be null.");
      }
      if (string.IsNullOrEmpty(Latitude))
      {
        yield return new ValidationResult("Latitude cannot be null.");
      }
      if (string.IsNullOrEmpty(Longitude))
      {
        yield return new ValidationResult("Longitude cannot be null or empty.");
      }
    }
  }
}
