using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  /// <summary>
  ///
  /// </summary>
  public class AddressModelTest
  {
    public static readonly IEnumerable<object[]> Addresses = new List<object[]>
    {
      new object[]
      {
        new AddressModel
        {
          Id = 0,
          City = "city",
          Country = "US", //US or USA are the only valid country names
          PostalCode = "postalcode",
          StateProvince = "stateprovince",
          Street = "street",
        }
      }
    };

    /// <summary>
    ///
    /// </summary>
    /// <param name="address"></param>
    [Theory]
    [MemberData(nameof(Addresses))]
    public void Test_Create_AddressModel(AddressModel address)
    {
      var validationContext = new ValidationContext(address);
      var actual = Validator.TryValidateObject(address, validationContext, null, true);

      Assert.True(actual);
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="address"></param>
    [Theory]
    [MemberData(nameof(Addresses))]
    public void Test_Validate_AddressModel(AddressModel address)
    {
      var validationContext = new ValidationContext(address);

      Assert.Empty(address.Validate(validationContext));
    }
  }
}
