using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;
using Xunit;
using System;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class RepositoryTest : DataTest
  {
    public static readonly IEnumerable<object[]> Records = new List<object[]>
    {
      new object[]
      {
        new LodgingModel { Id = 1, LocationId = 1, Bathrooms = 1, Name = "Test" },
        new RentalModel { Id = 1, LotNumber = "1", Price = 1.11, Status = "Available" },
        new ReviewModel { Id = 1, Comment = "Comment", DateCreated = DateTime.Now, Rating = 1 }
      }
    };

    [Theory]
    [MemberData(nameof(Records))]
    public async void Test_Repository_DeleteAsync(LodgingModel lodging, RentalModel rental, ReviewModel review)
    {
      using (var ctx = new LodgingContext(Options))
      {
        ctx.Rentals.RemoveRange(ctx.Rentals);
        ctx.Lodgings.RemoveRange(ctx.Lodgings);
        await ctx.Rentals.AddAsync(rental);
        await ctx.Reviews.AddAsync(review);
        await ctx.Lodgings.AddAsync(lodging);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(Options))
      {
        var lodgings = new Repository<LodgingModel>(ctx);

        await lodgings.DeleteAsync(lodging.Id);

        Assert.Equal(EntityState.Deleted, ctx.Entry(ctx.Lodgings.Find(lodging.Id)).State);
      }

      using (var ctx = new LodgingContext(Options))
      {
        var rentals = new Repository<RentalModel>(ctx);

        await rentals.DeleteAsync(rental.Id);

        Assert.Equal(EntityState.Deleted, ctx.Entry(ctx.Rentals.Find(rental.Id)).State);
      }

      using (var ctx = new LodgingContext(Options))
      {
        var reviews = new Repository<ReviewModel>(ctx);

        await reviews.DeleteAsync(review.Id);

        Assert.Equal(EntityState.Deleted, ctx.Entry(ctx.Reviews.Find(review.Id)).State);
      }
    }

    [Theory]
    [MemberData(nameof(Records))]
    public async void Test_Repository_InsertAsync(LodgingModel lodging, RentalModel rental, ReviewModel review)
    {
      using (var ctx = new LodgingContext(Options))
      {
        ctx.Rentals.RemoveRange(ctx.Rentals);
        ctx.Lodgings.RemoveRange(ctx.Lodgings);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(Options))
      {
        var lodgings = new Repository<LodgingModel>(ctx);

        await lodgings.InsertAsync(lodging);

        Assert.Equal(EntityState.Added, ctx.Entry(lodging).State);
      }

      using (var ctx = new LodgingContext(Options))
      {
        var rentals = new Repository<RentalModel>(ctx);

        await rentals.InsertAsync(rental);

        Assert.Equal(EntityState.Added, ctx.Entry(rental).State);
      }

      using (var ctx = new LodgingContext(Options))
      {
        var reviews = new Repository<ReviewModel>(ctx);

        await reviews.InsertAsync(review);

        Assert.Equal(EntityState.Added, ctx.Entry(review).State);
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync()
    {
      using (var ctx = new LodgingContext(Options))
      {
        ctx.Rentals.RemoveRange(ctx.Rentals);
        ctx.Lodgings.RemoveRange(ctx.Lodgings);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(Options))
      {
        var lodgings = new Repository<LodgingModel>(ctx);

        var actual = await lodgings.SelectAsync();

        Assert.Empty(actual);
      }

      using (var ctx = new LodgingContext(Options))
      {
        var rentals = new Repository<RentalModel>(ctx);

        var actual = await rentals.SelectAsync();

        Assert.Empty(actual);
      }

      using (var ctx = new LodgingContext(Options))
      {
        var reviews = new Repository<ReviewModel>(ctx);

        var actual = await reviews.SelectAsync();

        Assert.Empty(actual);
      }
    }

    [Fact]
    public async void Test_Repository_SelectAsync_ById()
    {
      using (var ctx = new LodgingContext(Options))
      {
        ctx.Rentals.RemoveRange(ctx.Rentals);
        ctx.Lodgings.RemoveRange(ctx.Lodgings);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(Options))
      {
        var lodgings = new Repository<LodgingModel>(ctx);

        var actual = await lodgings.SelectAsync(1);

        Assert.Null(actual);
      }

      using (var ctx = new LodgingContext(Options))
      {
        var rentals = new Repository<RentalModel>(ctx);

        var actual = await rentals.SelectAsync(1);

        Assert.Null(actual);
      }

      using (var ctx = new LodgingContext(Options))
      {
        var reviews = new Repository<ReviewModel>(ctx);

        var actual = await reviews.SelectAsync(1);

        Assert.Null(actual);
      }
    }

    [Theory]
    [MemberData(nameof(Records))]
    public async void Test_Repository_Update(LodgingModel lodging, RentalModel rental, ReviewModel review)
    {
      using (var ctx = new LodgingContext(Options))
      {
        ctx.Rentals.RemoveRange(ctx.Rentals);
        ctx.Lodgings.RemoveRange(ctx.Lodgings);
        await ctx.Lodgings.AddAsync(lodging);
        await ctx.Rentals.AddAsync(rental);
        await ctx.Reviews.AddAsync(review);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(Options))
      {
        var lodgings = new Repository<LodgingModel>(ctx);
        var lodgingToUpdate = await ctx.Lodgings.FirstAsync();

        lodgingToUpdate.Name = "Name";
        lodgings.Update(lodgingToUpdate);

        var result = ctx.Lodgings.Find(lodging.Id);
        Assert.Equal(lodgingToUpdate.Name, result.Name);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }

      using (var ctx = new LodgingContext(Options))
      {
        var rentals = new Repository<RentalModel>(ctx);
        var rentalToUpdate = await ctx.Rentals.FirstAsync();

        rentalToUpdate.LotNumber = "4";
        rentals.Update(rentalToUpdate);

        var result = ctx.Rentals.Find(rental.Id);
        Assert.Equal(rentalToUpdate.LotNumber, result.LotNumber);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }

      using (var ctx = new LodgingContext(Options))
      {
        var reviews = new Repository<ReviewModel>(ctx);
        var reviewToUpdate = await ctx.Reviews.FirstAsync();

        reviewToUpdate.Comment = "Comment";
        reviews.Update(reviewToUpdate);

        var result = ctx.Reviews.Find(review.Id);
        Assert.Equal(reviewToUpdate.Comment, result.Comment);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }
    }
  }
}
