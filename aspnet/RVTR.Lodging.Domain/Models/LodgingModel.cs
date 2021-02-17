using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Attributes;

namespace RVTR.Lodging.Domain.Models
{
  /// <summary>
  /// Represents the _Lodging_ model
  /// </summary>
  public class LodgingModel : IValidatableObject
  {

    /// <summary>
    /// id of the lodging model in the db
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    /// <summary>
    /// Location id of the lodging's location
    /// </summary>
    /// <value></value>
    public int LocationId { get; set; }

    /// <summary>
    /// Location property of the lodging model (required)
    /// </summary>
    /// <value></value>
    public AddressModel Address { get; set; }

    /// <summary>
    /// Name of the lodging (required)
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Max length is 100 characters")]
    public string Name { get; set; }

    /// <summary>
    /// Number of bathrooms at the lodging
    /// </summary>
    /// <value></value>
    [FacilitiesAttribute]
    public int Bathrooms { get; set; }

    /// <summary>
    /// Rental list of the lodging
    /// </summary>
    /// <value></value>
    public IEnumerable<RentalModel> Rentals { get; set; } = new List<RentalModel>();

    /// <summary>
    /// Review list for the lodging
    /// </summary>
    /// <value></value>
    public IEnumerable<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();

    /// <summary>
    /// Review list for the images
    /// </summary>
    /// <value></value>
    public IEnumerable<ImageModel> Images { get; set; } = new List<ImageModel>();

    /// <summary>
    /// Represents the _Lodging_ `Validate` model
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
