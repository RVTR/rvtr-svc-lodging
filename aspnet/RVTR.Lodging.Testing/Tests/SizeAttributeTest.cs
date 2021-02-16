using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Models;
using RVTR.Lodging.Domain.Attributes;
using Xunit;
using System.Linq;

namespace RVTR.Lodging.Testing.Tests
{
  public class SizeAttributeTest
  {
    public static readonly string GoodSize = "10ft x 10ft";
    public static readonly List<string> BadSize = new List<string>
    {
        null,
        "10 x 10"
    };
    public SizeAttribute SizeAttribute = new SizeAttribute();

    [Fact]
    public void Test_SizeAttribute_Good()
    {
      var actual = SizeAttribute.IsValid(GoodSize);

      Assert.True(actual);
    }
    [Fact]
    public void Test_SizeAttribute_Bad()
    {
      bool actual = true;
      foreach(var item in BadSize)
      {
        actual = SizeAttribute.IsValid(item);
        Assert.False(actual);
      }
    }
  }
}
