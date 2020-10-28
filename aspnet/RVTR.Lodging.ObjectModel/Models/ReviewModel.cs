using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Review_ model
  /// </summary>
  public class ReviewModel : IValidatableObject
  {
    public int Id { get; set; }

    public int AccountId { get; set; }

    public string Comment { get; set; } //Comment can be empty

    public DateTime DateCreated { get; set; }

    public int Rating { get; set; } = -1;

    public int? LodgingId { get; set; }

    public LodgingModel Lodging { get; set; }

    /// <summary>
    /// Represents the _Review_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Lodging == null)
      {
        yield return new ValidationResult("Lodging object cannot be null.");
      }
      if (DateCreated == null)
      {
        yield return new ValidationResult("DateCreated cannot be null.");
      }
      if (Rating < 1 || Rating > 10)
      {
        yield return new ValidationResult("Rating must be between 1 and 10.");
      }
    }
  }
}
