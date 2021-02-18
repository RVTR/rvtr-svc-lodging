using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Attributes;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class CheckInAttributeTest
  {
    private static readonly string _errorMessage = "CheckIn required to be true.";

    [Fact]
    public void Test_Attribute_IsValid_True()
    {
      var input = true;
      var attribute = new CheckInAttribute();

      var result = attribute.IsValid(input);

      Assert.True(result);
    }

    [Fact]
    public void Test_Attribute_IsValid_False()
    {
      var input = false;
      var attribute = new CheckInAttribute();

      var result = attribute.IsValid(input);
      var validationResult = attribute.GetValidationResult(input, new ValidationContext(input));
      var errorMessage = validationResult.ErrorMessage;

      Assert.False(result);
      Assert.Equal(_errorMessage, errorMessage);
    }
  }
}
