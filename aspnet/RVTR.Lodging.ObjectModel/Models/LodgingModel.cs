using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Lodging_ model
  /// </summary>
  public class LodgingModel : IValidatableObject
  {
    public int Id { get; set; }

    public LocationModel Location { get; set; }

    [Required]
    public string Name { get; set; }

    public IEnumerable<RentalModel> Rentals { get; set; }

    public IEnumerable<ReviewModel> Reviews { get; set; }

    /// <summary>
    /// Represents the _Lodging_ `Validate` model
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
