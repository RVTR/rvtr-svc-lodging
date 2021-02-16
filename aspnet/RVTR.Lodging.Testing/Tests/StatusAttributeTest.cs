using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Models;
using RVTR.Lodging.Domain.Attributes;
using Xunit;
using System.Linq;

namespace RVTR.Lodging.Testing.Tests
{
  public class StatusAttributeTest
  {
    public static readonly string GoodStatus = "Booked";
    public static readonly List<string> BadStatus = new List<string>
    {
        null,
        "Booked123"
    };
    public StatusAttribute StatusAttribute = new StatusAttribute();

    [Fact]
    public void Test_StatusAttribute_Good()
    {
      var actual = StatusAttribute.IsValid(GoodStatus);

      Assert.True(actual);
    }
    [Fact]
    public void Test_StatusAttribute_Bad()
    {
      bool actual = true;
      foreach(var item in BadStatus)
      {
        actual = StatusAttribute.IsValid(item);
        Assert.False(actual);
      }
    }
  }
}
