using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Abstracts;

namespace RVTR.Lodging.Domain.Models
{
  public class ImageModel : AEntity, IValidatableObject
  {
    /// <summary>
    /// Lodging model id (foreign key)
    /// </summary>
    public int? LodgingModelId { get; set; }

    /// <summary>
    /// Image uri for the image
    /// </summary>
    [RegularExpression(@"^(http(s?):\/\/)[^\s]*$", ErrorMessage = "Image URI must be a real image URI.")]
    public string ImageUri { get; set; } = "https://bulma.io/images/placeholders/1280x960.png";

    /// <summary>
    /// Represents the _Rental_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
