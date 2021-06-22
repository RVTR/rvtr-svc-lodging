using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RVTR.Lodging.Context;
using RVTR.Lodging.Context.Repositories;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class UnitOfWorkTest : DataTest
  {
    [Fact]
    public async void TestUnitOfWorkCommitAsync()
    {
      using var ctx = new LodgingContext(Options);
      var unitOfWork = new UnitOfWork(ctx);
      var actual = await unitOfWork.CommitAsync();

      Assert.NotNull(unitOfWork.Lodging);
      Assert.NotNull(unitOfWork.Rental);
      Assert.NotNull(unitOfWork.Review);
      Assert.Equal(0, actual);
    }
  }
}
