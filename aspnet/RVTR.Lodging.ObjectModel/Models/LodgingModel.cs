using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Lodging_ model
  /// </summary>
  public class LodgingModel : IValidatableObject
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
    public int LocationId { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public LocationModel Location { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public int Bathrooms { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public IEnumerable<RentalModel> Rentals { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public IEnumerable<ReviewModel> Reviews { get; set; }

    /// <summary>
    /// Represents the _Lodging_ `Validate` model
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Location == null)
      {
        yield return new ValidationResult("Location cannot be null.");
      }
      if (string.IsNullOrEmpty(Name))
      {
        yield return new ValidationResult("Name cannot be null.");
      }
      if (Bathrooms < 0)
      {
        yield return new ValidationResult("Number of bathrooms must be greater than 0.");
      }
    }
  }
}
