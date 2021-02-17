using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Models;
using RVTR.Lodging.Domain.Attributes;
using Xunit;
using System.Linq;

namespace RVTR.Lodging.Testing.Tests
{
  public class FacilitiesNumberAttributeTest
  {
    public static readonly int GoodFacilitiesNumber = 1;
    public static readonly int BadFacilitiesNumber = 0;

    public FacilitiesAttribute FacilitiesAttribute = new FacilitiesAttribute();

    [Fact]
    public void Test_FacilitiesNumberAttribute_Good()
    {
      var actual = FacilitiesAttribute.IsValid(GoodFacilitiesNumber);

      Assert.True(actual);
    }
    [Fact]
    public void Test_FacilitiesNumberAttribute_Bad()
    {
        bool actual = true;

        actual = FacilitiesAttribute.IsValid(BadFacilitiesNumber);
        Assert.False(actual);

    }
  }
}
