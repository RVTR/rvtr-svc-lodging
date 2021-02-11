using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Attributes;

namespace RVTR.Lodging.Domain.Models
{
  /// <summary>
  /// Represents the _Review_ model
  /// </summary>
  public class ReviewModel : IValidatableObject
  {
    public int Id { get; set; }

    public int AccountId { get; set; }

    [LengthAttribute(1, 1000, "Must have a comment.")]
    public string Comment { get; set; }

    [Required(ErrorMessage = "Timestamp can't be null.")]
    public DateTime DateCreated { get; set; }

    [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10")]
    public int Rating { get; set; }

    public int? LodgingModelId { get; set; }

    /// <summary>
    /// The name of the profile reviewing the lodging
    /// </summary>
    [LengthAttribute(1, 1000, "Must have a name.")]
    public string Name { get; set; }

    /// <summary>
    /// Represents the _Review_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
