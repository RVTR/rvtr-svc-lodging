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

    [Required(ErrorMessage = "Must have a comment.")]
    [MaxLength(1000, ErrorMessage = "Comment must be fewer than 1000 characters long")]
    public string Comment { get; set; }

    [Required(ErrorMessage = "Timestamp can't be null.")]
    public DateTime DateCreated { get; set; }

    [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10")]
    public int Rating { get; set; }

    [CheckInAttribute]
    public bool IsCheckIn { get; set; }

    public int? LodgingModelId { get; set; }

    /// <summary>
    /// The name of the profile reviewing the lodging
    /// </summary>
    [Required(ErrorMessage = "Must have a name.")]
    [MaxLength(100, ErrorMessage = "Comment must be fewer than 100 characters long")]
    public string Name { get; set; }

    /// <summary>
    /// Represents the _Review_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
