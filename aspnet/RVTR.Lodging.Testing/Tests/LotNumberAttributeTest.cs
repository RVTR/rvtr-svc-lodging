using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Models;
using RVTR.Lodging.Domain.Attributes;
using Xunit;
using System.Linq;

namespace RVTR.Lodging.Testing.Tests
{
  public class LotNumberAttributeTest
  {
    private static readonly string GoodLotNumber = "1";
    public static readonly List<string> BadLotNumber = new List<string>
    {
        null,
        "123LotNumberwithmorethan10digit",
        "nonumbers"
    };
    public LotNumberAttribute LotNumberAttribute = new LotNumberAttribute();

    [Fact]
    public void TestLotNumberAttributeGood()
    {
      var actual = LotNumberAttribute.IsValid(GoodLotNumber);

      Assert.True(actual);
    }
    [Fact]
    public void TestLotNumberAttributeBad()
    {
      bool actual = true;
      foreach (var lotnumber in BadLotNumber)
      {
        actual = LotNumberAttribute.IsValid(lotnumber);
        Assert.False(actual);
      }
    }
  }
}
