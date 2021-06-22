using System;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Attributes;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class CheckInAttributeTest
  {
    private static readonly string _errorMessage = "Check In date has not occurred.";

    [Fact]
    public void TestAttributeIsValidTrue()
    {
      var input = DateTime.Now.AddDays(-1);
      var attribute = new CheckInAttribute();

      var result = attribute.IsValid(input);

      Assert.True(result);
    }

    [Fact]
    public void TestAttributeIsValidFalse()
    {
      var input = DateTime.Now.AddDays(1);
      var attribute = new CheckInAttribute();

      var result = attribute.IsValid(input);
      var validationResult = attribute.GetValidationResult(input, new ValidationContext(input));
      var errorMessage = validationResult.ErrorMessage;

      Assert.False(result);
      Assert.Equal(_errorMessage, errorMessage);
    }
  }
}
