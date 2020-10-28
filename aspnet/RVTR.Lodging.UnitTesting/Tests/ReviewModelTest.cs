using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
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
          Comment = "comment",
          DateCreated = DateTime.Now,
          Rating = 1, //Rating must be between 1 and 10
          LodgingId = 0,
          Lodging = new LodgingModel()
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
