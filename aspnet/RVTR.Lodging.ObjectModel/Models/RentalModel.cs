using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  /// Represents the _Rental_ model
  /// </summary>
  public class RentalModel : IValidatableObject
  {
    public int Id { get; set; }

    public string LotNumber { get; set; }

    public RentalUnitModel Unit { get; set; }

    public string Status { get; set; }

    //Instantiates to -1, which is an invalid price. That way when validated, you must change it to a valid price.
    public double Price { get; set; } = -1;

    public double? DiscountedPrice { get; set; }

    public int? LodgingId { get; set; }

    public LodgingModel Lodging { get; set; }

    /// <summary>
    /// Represents the _Rental_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Lodging == null)
      {
        yield return new ValidationResult("Lodging cannot be null.");
      }
      if (Price < 0) //If the price is less than 0, it is invalid
      {
        yield return new ValidationResult("Price must be greater than 0.");
      }
      if (string.IsNullOrEmpty(Status))
      {
        yield return new ValidationResult("Status cannot be null or empty.");
      }
      if (string.IsNullOrEmpty(LotNumber))
      {
        yield return new ValidationResult("Lot number cannot be null or empty.");
      }
    }
  }
}
