using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Models;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class LocationModelTest
  {
    public static readonly IEnumerable<object[]> Locations = new List<object[]>
    {
      new object[]
      {
        new LocationModel
        {
          Id = 0,
          Address = new AddressModel(),
          Latitude = "00.00",
          Longitude = "00.00"
        }
      }
    };

    [Theory]
    [MemberData(nameof(Locations))]
    public void Test_Create_LocationModel(LocationModel location)
    {
      var validationContext = new ValidationContext(location);
      var actual = Validator.TryValidateObject(location, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(Locations))]
    public void Test_Validate_LocationModel(LocationModel location)
    {
      var validationContext = new ValidationContext(location);

      Assert.Empty(location.Validate(validationContext));
    }
  }
}
