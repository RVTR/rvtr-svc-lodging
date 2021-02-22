using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RVTR.Lodging.Context;
using RVTR.Lodging.Context.Repositories;
using RVTR.Lodging.Domain.Models;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class LodgingRepoTest : DataTest
  {
    public static readonly IEnumerable<object[]> Records = new List<object[]>
    {
      new object[]
      {
        new LodgingModel
        {
          Id = 5,
          Name = "Lodging",
          Address = new AddressModel
          {
            Id = 100, City = "Austin", StateProvince = "TX", Country = "USA", Longitude = "1.00N", Latitude = "1.00W", PostalCode = "11111", Street = "Street"
          },
          Rentals = new List<RentalModel>
          {
            new RentalModel()
            {
              Id = 100, LotNumber = "1", Status = "Available", SiteName = "Unit1", Size = "5x5", Capacity = 4
            },
            new RentalModel()
            {
              Id = 101, LotNumber = "2", Status = "Booked", SiteName = "Unit2", Size = "5x5", Capacity = 4
            }
          }
        }
      }
    };

    [Theory]
    [MemberData(nameof(Records))]
    public async void Test_LodgingRepo_SelectAsync(LodgingModel lodging)
    {
      using (var ctx = new LodgingContext(Options))
      {
        await ctx.Lodgings.AddAsync(lodging);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(Options))
      {
        var lodgings = new LodgingRepo(ctx);

        var resultEqual = await lodgings.SelectAsync(5);

        Assert.Equal(lodging.Id, resultEqual.Id);
        Assert.Equal(lodging.Bathrooms, resultEqual.Bathrooms);
        Assert.Equal(lodging.Address.Latitude, resultEqual.Address.Latitude);
        Assert.Equal(lodging.Address.Longitude, resultEqual.Address.Longitude);
        Assert.Equal(lodging.Name, resultEqual.Name);

        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await lodgings.SelectAsync(-1));
      }
    }

    [Theory]
    [MemberData(nameof(Records))]
    public async void Test_LodgingRepo_SelectAllAsync(LodgingModel lodging)
    {
      using (var ctx = new LodgingContext(Options))
      {
        var lodgings = new LodgingRepo(ctx);

        var result = await lodgings.SelectAsync();

        Assert.IsType<List<LodgingModel>>(result);

        await Assert.ThrowsAsync<KeyNotFoundException>(async () => await lodgings.SelectAsync(-1));
      }
    }

    [Theory]
    [MemberData(nameof(Records))]
    public async void Test_LodgingRepo_LodgingByLocationAndOccupancy(LodgingModel lodging)
    {
      using (var ctx = new LodgingContext(Options))
      {
        await ctx.Lodgings.AddAsync(lodging);
        await ctx.SaveChangesAsync();
      }

      using (var ctx = new LodgingContext(Options))
      {
        var lodgings = new LodgingRepo(ctx);

        var actual = await lodgings.LodgingByLocationAndOccupancy(2, "Austin");

        Assert.NotEmpty(actual);
        Assert.True(actual.Count() == 1);
      }
    }
  }
}
