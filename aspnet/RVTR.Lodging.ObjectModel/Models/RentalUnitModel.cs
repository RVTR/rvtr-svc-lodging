using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents the _RentalUnit_ model
  /// </summary>
  public class RentalUnitModel : IValidatableObject
  {
    public int Id { get; set; }

    public IEnumerable<BathroomModel> Bathrooms { get; set; }

    public IEnumerable<BedroomModel> Bedrooms { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public int Occupancy { get; set; }

    [Required]
    public string RentalUnitType { get; set; }

    /// <summary>
    /// Represents the _RentalUnit_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
