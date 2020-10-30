using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

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
    /// List of the image picture urls that a lodging can display 
    /// </summary>
    public IEnumerable<ImageModel> Images { get; set; }

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
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
