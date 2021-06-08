using System.Collections.Generic;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.Context;
using RVTR.Lodging.Context.Repositories;
using RVTR.Lodging.Domain.Models;
using Xunit;
using System;

namespace RVTR.Lodging.Testing.Tests
{
  public class RepositoryTest : DataTest
  {
    public static readonly IEnumerable<object[]> Records = new List<object[]>
    {
      new object[]
      {
        new LodgingModel { EntityId = 1, AddressId = 1, Bathrooms = 1, Name = "Test" },
        new RentalModel { EntityId = 1, LotNumber = "1", Price = 1.11, Status = "Available" },
        new ReviewModel { EntityId = 1, Comment = "Comment", DateCreated = DateTime.Now, Rating = 1, Name = "Bob" },
        new ImageModel { EntityId = 1, ImageUri = "" }
      }
    };

    [Theory]
    [MemberData(nameof(Records))]
    public async void TestRepositoryDeleteAsync(LodgingModel lodging, RentalModel rental, ReviewModel review, ImageModel image)
    {
      using (var ctx = new LodgingContext(Options))
      {
        ctx.Rentals.RemoveRange(ctx.Rentals);
        ctx.Lodgings.RemoveRange(ctx.Lodgings);
        ctx.Images.RemoveRange(ctx.Images);

        await ctx.Rentals.AddAsync(rental);
        await ctx.Reviews.AddAsync(review);
        await ctx.Images.AddAsync(image);
        await ctx.Lodgings.AddAsync(lodging);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(Options))
      {
        var lodgings = new Repository<LodgingModel>(ctx);

        await lodgings.DeleteAsync(lodging.EntityId);

        Assert.Equal(EntityState.Deleted, ctx.Entry(ctx.Lodgings.Find(lodging.EntityId)).State);
      }

      using (var ctx = new LodgingContext(Options))
      {
        var rentals = new Repository<RentalModel>(ctx);

        await rentals.DeleteAsync(rental.EntityId);

        Assert.Equal(EntityState.Deleted, ctx.Entry(ctx.Rentals.Find(rental.EntityId)).State);
      }

      using (var ctx = new LodgingContext(Options))
      {
        var reviews = new Repository<ReviewModel>(ctx);

        await reviews.DeleteAsync(review.EntityId);

        Assert.Equal(EntityState.Deleted, ctx.Entry(ctx.Reviews.Find(review.EntityId)).State);
      }
      using (var ctx = new LodgingContext(Options))
      {
        var images = new Repository<ImageModel>(ctx);

        await images.DeleteAsync(image.EntityId);

        Assert.Equal(EntityState.Deleted, ctx.Entry(ctx.Images.Find(image.EntityId)).State);
      }
    }

    [Theory]
    [MemberData(nameof(Records))]
    public async void TestRepositoryInsertAsync(LodgingModel lodging, RentalModel rental, ReviewModel review, ImageModel image)
    {
      using (var ctx = new LodgingContext(Options))
      {
        ctx.Rentals.RemoveRange(ctx.Rentals);
        ctx.Lodgings.RemoveRange(ctx.Lodgings);
        ctx.Images.RemoveRange(ctx.Images);

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
      using (var ctx = new LodgingContext(Options))
      {
        var images = new Repository<ImageModel>(ctx);

        await images.InsertAsync(image);

        Assert.Equal(EntityState.Added, ctx.Entry(image).State);
      }
    }

    [Fact]
    public async void TestRepositorySelectAsync()
    {
      using (var ctx = new LodgingContext(Options))
      {
        ctx.Rentals.RemoveRange(ctx.Rentals);
        ctx.Lodgings.RemoveRange(ctx.Lodgings);
        ctx.Images.RemoveRange(ctx.Images);

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
      using (var ctx = new LodgingContext(Options))
      {
        var images = new Repository<ImageModel>(ctx);
        var actual = await images.SelectAsync();

        Assert.Empty(actual);
      }
    }

    [Theory]
    [MemberData(nameof(Records))]
    public async void TestRepositoryUpdate(LodgingModel lodging, RentalModel rental, ReviewModel review, ImageModel image)
    {
      using (var ctx = new LodgingContext(Options))
      {
        ctx.Rentals.RemoveRange(ctx.Rentals);
        ctx.Lodgings.RemoveRange(ctx.Lodgings);
        ctx.Images.RemoveRange(ctx.Images);

        await ctx.Lodgings.AddAsync(lodging);
        await ctx.Rentals.AddAsync(rental);
        await ctx.Reviews.AddAsync(review);
        await ctx.Images.AddAsync(image);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(Options))
      {
        var lodgings = new Repository<LodgingModel>(ctx);
        var lodgingToUpdate = await ctx.Lodgings.FirstAsync();

        lodgingToUpdate.Name = "Name";
        lodgings.Update(lodgingToUpdate);

        var result = ctx.Lodgings.Find(lodging.EntityId);
        Assert.Equal(lodgingToUpdate.Name, result.Name);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }

      using (var ctx = new LodgingContext(Options))
      {
        var rentals = new Repository<RentalModel>(ctx);
        var rentalToUpdate = await ctx.Rentals.FirstAsync();

        rentalToUpdate.LotNumber = "4";
        rentals.Update(rentalToUpdate);

        var result = ctx.Rentals.Find(rental.EntityId);
        Assert.Equal(rentalToUpdate.LotNumber, result.LotNumber);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }

      using (var ctx = new LodgingContext(Options))
      {
        var reviews = new Repository<ReviewModel>(ctx);
        var reviewToUpdate = await ctx.Reviews.FirstAsync();

        reviewToUpdate.Comment = "Comment";
        reviews.Update(reviewToUpdate);

        var result = ctx.Reviews.Find(review.EntityId);
        Assert.Equal(reviewToUpdate.Comment, result.Comment);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }

      using (var ctx = new LodgingContext(Options))
      {
        var images = new Repository<ImageModel>(ctx);
        var imageToUpdate = await ctx.Images.FirstAsync();

        imageToUpdate.ImageUri = "https://test.jpg";
        images.Update(imageToUpdate);

        var result = ctx.Images.Find(image.EntityId);
        Assert.Equal(imageToUpdate.ImageUri, result.ImageUri);
        Assert.Equal(EntityState.Modified, ctx.Entry(result).State);
      }
    }
  }
}
