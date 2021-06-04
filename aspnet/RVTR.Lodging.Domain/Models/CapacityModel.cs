using RVTR.Lodging.Domain.Abstracts;

namespace RVTR.Lodging.Domain.Models
{
  /// <summary>
  /// Represents the _Capacity_ model
  /// </summary>
  public class CapacityModel : AEntity
  {
    /// <summary>
    /// The type of Capacity (Cars, Pets, etc.)
    /// </summary>
    /// <value></value>
    public string Type { get; set; }

    /// <summary>
    /// The quantity of the type of capacity allowed
    /// </summary>
    /// <value></value>

    public int Quanitity { get; set; }
  }
}