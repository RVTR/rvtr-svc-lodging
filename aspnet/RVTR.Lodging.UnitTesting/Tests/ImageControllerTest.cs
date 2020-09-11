using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Lodging.DataContext;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;
using RVTR.Lodging.WebApi.Controllers;
using Xunit;

namespace RVTR.Lodging.UnitTesting.Tests
{
  public class ImageControllerTest
  {
    private static readonly SqliteConnection _connection = new SqliteConnection("Data Source=:memory:");
    private static readonly DbContextOptions<LodgingContext> _options = new DbContextOptionsBuilder<LodgingContext>().UseSqlite(_connection).Options;
    private readonly LodgingController _controller;
    private readonly ILogger<LodgingController> _logger;
    private readonly UnitOfWork _unitOfWork;

    public ImageControllerTest()
    {
      var contextMock = new Mock<LodgingContext>(_options);
      var loggerMock = new Mock<ILogger<LodgingController>>();
      var repositoryMock = new Mock<LodgingRepo>(new LodgingContext(_options));
      var unitOfWorkMock = new Mock<UnitOfWork>(contextMock.Object);

      repositoryMock.Setup(m => m.SelectAsync()).Returns(Task.FromResult<IEnumerable<LodgingModel>>(null));
      repositoryMock.Setup(m => m.SelectAsync(0)).Returns(Task.FromResult(new LodgingModel()));
      repositoryMock.Setup(m => m.SelectAsync(1)).Returns(Task.FromResult<LodgingModel>(null));
      unitOfWorkMock.Setup(m => m.Lodging).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new ImageController(_logger, _unitOfWork);
    }

    [Fact]
    public async void Test_Controller_Get()
    {
      var resultMany = await _controller.Get();

      Assert.NotNull(resultMany);
    }
  }
}
