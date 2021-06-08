using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Abstracts;
using RVTR.Lodging.Domain.Attributes;

namespace RVTR.Lodging.Domain.Models
{
  /// <summary>
  /// Represents the _Rental_ model
  /// </summary>
  public class RentalModel : AEntity, IValidatableObject
  {
    /// <summary>
    /// The lot number of the rental model
    /// </summary>
    /// <value></value>
    [LotNumberAttribute]
    public string LotNumber { get; set; }

    /// <summary>
    /// The status of the rental model
    /// </summary>
    /// <value></value>
    [StatusAttribute]
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
    public List<CapacityModel> Capacity { get; set; }
    public List<int?> CapacityId { get; set; }

    /// <summary>
    /// The name of the campsite
    /// </summary>
    /// <value></value>
    [SiteNameAttribute]
    public string SiteName { get; set; }

    /// <summary>
    /// the size of the campsite (e.g. 5 x 5, 5x5, 5ft x 5ft, 5 yards x 5 yards etc.)
    /// </summary>
    /// <value></value>
    [SizeAttribute]
    public string Size { get; set; }

    /// <summary>
    /// Represents the _Rental_ `Validate` method
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) => new List<ValidationResult>();
  }
}
