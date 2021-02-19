using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.Domain.Models;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class ReviewModelTest
  {
    public static readonly IEnumerable<object[]> Reviews = new List<object[]>
    {
      new object[]
      {
        new ReviewModel
        {
          Id = 0,
          AccountId = 0,
          Comment = "Comment",
          DateCreated = DateTime.Now,
          Rating = 1,
          CheckIn = new DateTime(2010, 1, 1),
          LodgingModelId = 0,
          Name = "Bob"
        }
      }
    };

    [Theory]
    [MemberData(nameof(Reviews))]
    public void Test_Create_ReviewModel(ReviewModel review)
    {
      var validationContext = new ValidationContext(review);
      var actual = Validator.TryValidateObject(review, validationContext, null, true);

      Assert.True(actual);
    }

    [Theory]
    [MemberData(nameof(Reviews))]
    public void Test_Validate_ReviewModel(ReviewModel review)
    {
      var validationContext = new ValidationContext(review);

      Assert.Empty(review.Validate(validationContext));
    }
  }
}
