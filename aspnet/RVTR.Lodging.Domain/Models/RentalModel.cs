using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.Domain.Models
{
  /// <summary>
  /// Represents the _Rental_ model
  /// </summary>
  public class RentalModel : IValidatableObject
  {
    /// <summary>
    /// The Id of the rental model
    /// </summary>
    /// <value></value>
    public int Id { get; set; }

    /// <summary>
    /// The lot number of the rental model
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Lot number can't be null.")]
    [MaxLength(10, ErrorMessage = "Lot number must be 10 digits maximum")]
    [RegularExpression(@"^\d+([a-zA-Z]+)?$", ErrorMessage = "Lot number must be either a number or a number plus a series of letters.")]
    public string LotNumber { get; set; }

    /// <summary>
    /// The status of the rental model
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Status can't be null.")]
    [RegularExpression(@"^([Bb]ooked|[Aa]vailable)$", ErrorMessage = "Status must be either 'Booked' or 'Available'")]
    public string Status { get; set; }

    /// <summary>
    /// The price of the rental model
    /// </summary>
    /// <value></value>
    [Range(0, Double.PositiveInfinity, ErrorMessage = "Price must be positive.")]
    public double Price { get; set; }

    /// <summary>
    /// The discounted price of the rental model
    /// </summary>
    /// <value></value>
    [Range(0, Double.PositiveInfinity, ErrorMessage = "Price must be positive.")]
    public double? DiscountedPrice { get; set; }

    /// <summary>
    /// The Id of Lodging Model
    /// </summary>
    /// <value></value>
    public int? LodgingModelId { get; set; }

    /// <summary>
    /// The capacity of the campsite
    /// </summary>
    /// <value></value>
    [Range(1, 1000, ErrorMessage = "Capacity must be between 1 and 1000")]
    public int Capacity { get; set; }

    /// <summary>
    /// The name of the campsite
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Name must exist.")]
    [MaxLength(100, ErrorMessage = "Name must be fewer than 100 characters")]
    public string SiteName { get; set; }

    /// <summary>
    /// the size of the campsite (e.g. 5 x 5, 5x5, 5ft x 5ft, 5 yards x 5 yards etc.)
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Size must exist")]
    [RegularExpression(@"^\d+ ?([Ff]t|[Yy]ards|[Mm]eters|[Mm]) ?x ?\d+ ?([Ff]t|[Yy]ards|[Mm]eters|[Mm])$", ErrorMessage = "Size must be in the form '10 [unit?] x 10 [unit?]'")]
    public string Size { get; set; }

    /// <summary>
    /// Represents the _Rental_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
