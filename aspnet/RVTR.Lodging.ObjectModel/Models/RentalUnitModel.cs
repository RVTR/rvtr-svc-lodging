using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.ObjectModel.Models
{
  /// <summary>
  ///
  /// </summary>
  public class RentalUnitModel : IValidatableObject
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
    public int RentalId { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public int Capacity { get; set; } = -1; //instantiate to an invalid numer

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public string Name { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public RentalModel Rental { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <value></value>
    public string Size { get; set; }

    /// <summary>
    ///
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      if (Capacity < 0)
      {
        yield return new ValidationResult("Capacity must be greater than 0.");
      }
      if (Rental == null)
      {
        yield return new ValidationResult("Rental cannot be null.");
      }
      if (string.IsNullOrEmpty(Name))
      {
        yield return new ValidationResult("Name cannot be null or empty.");
      }
      if (string.IsNullOrEmpty(Size))
      {
        yield return new ValidationResult("Size cannot be null or empty.");
      }
    }
  }
}
