using RVTR.Lodging.Domain.Abstracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RVTR.Lodging.Domain.Models
{
  /// <summary>
  /// Represents the _Capacity_ model
  /// </summary>
  public class CapacityModel : AEntity
  {
    public List<int> RentalId { get; set; }

    /// <summary>
    /// The List of rental object
    /// </summary>
    /// <value></value>
    public List<RentalModel> Rental { get; set; }
    /// <summary>
    /// The type of Capacity (Cars, Pets, etc.)
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Type is required")]
    public string Type { get; set; }

    /// <summary>
    /// The quantity of the type of capacity allowed
    /// </summary>
    /// <value></value>
    [Required(ErrorMessage = "Quanitity is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Quanitity must be positive")]
    public int Quanitity { get; set; }
  }
}