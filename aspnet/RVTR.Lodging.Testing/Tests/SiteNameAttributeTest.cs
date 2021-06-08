using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Models;
using RVTR.Lodging.Domain.Attributes;
using Xunit;
using System.Linq;

namespace RVTR.Lodging.Testing.Tests
{
  public class SiteNameAttributeTest
  {
    private static readonly string GoodSiteName = "1";
    public static readonly List<string> BadSiteName = new List<string>
    {
        null,
        new string('*',101)
    };
    public SiteNameAttribute SiteNameAttribute = new SiteNameAttribute();

    [Fact]
    public void TestSiteNameAttributeGood()
    {
      var actual = SiteNameAttribute.IsValid(GoodSiteName);

      Assert.True(actual);
    }
    [Fact]
    public void TestSiteNameAttributeBad()
    {
      bool actual = true;
      foreach (var item in BadSiteName)
      {
        actual = SiteNameAttribute.IsValid(item);
        Assert.False(actual);
      }
    }
  }
}
