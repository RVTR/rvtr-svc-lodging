using System;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Attributes;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class CheckInAttributeTest
  {
    private static readonly string _errorEarly = "Check In date has not occurred.";
    private static readonly string _errorRequired = "Check In date required.";

    [Fact]
    public void Test_Attribute_IsValid_True()
    {
      var input = DateTime.Now.AddDays(-1);

      var attribute = new CheckInAttribute();

      var result = attribute.IsValid(input);

      Assert.True(result);
    }

    [Fact]
    public void Test_Attribute_IsValid_False()
    {
      var input = DateTime.Now.AddDays(1);
      var attribute = new CheckInAttribute();

      var resultEarly = attribute.IsValid(input);
      var validationEarly = attribute.GetValidationResult(input, new ValidationContext(input));
      var errorEarly = validationEarly.ErrorMessage;

      var resultRequired = attribute.IsValid(null);
      var validationRequired = attribute.GetValidationResult(null, new ValidationContext(null));
      var errorRequired = validationRequired.ErrorMessage;

      Assert.False(resultEarly);
      Assert.False(resultRequired);
      Assert.Equal(_errorEarly, errorEarly);
      Assert.Equal(_errorRequired, errorRequired);
    }
  }
}
