using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Lodging.Domain.Interfaces;
using RVTR.Lodging.Domain.Models;
using RVTR.Lodging.Service.Controllers;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class LodgingControllerTest
  {
    private readonly LodgingController _controller;
    private readonly ILogger<LodgingController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public LodgingControllerTest()
    {
      var loggerMock = new Mock<ILogger<LodgingController>>();
      var repositoryMock = new Mock<IRepository<LodgingModel>>();
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      repositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync((IEnumerable<LodgingModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == -1)).Throws(new KeyNotFoundException());
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == 0)).ReturnsAsync(new List<LodgingModel>());
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == 1)).ReturnsAsync((IEnumerable<LodgingModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == 2)).ReturnsAsync(new[] { new LodgingModel() { EntityId = 2, AddressId = 2, Name = "name", Bathrooms = 1 } });
      unitOfWorkMock.Setup(m => m.Lodging).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new LodgingController(_logger, _unitOfWork);
    }

    [Fact]
    public async void TestControllerGet()
    {
      var resultMany = await _controller.Get();

      Assert.NotNull(resultMany);
    }

    [Fact]
    public async void TestControllerGetID()
    {
      var failResult = await _controller.Get(-1);
      var returnOneResult = await _controller.Get(2);

      Assert.NotNull(failResult);
      Assert.NotNull(returnOneResult);
    }

    [Fact]
    public async void TestControllerDelete()
    {
      var resultPass = await _controller.Delete(-1);
      var resultFail = await _controller.Delete(2);

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void TestControllerPost()
    {
      var resultPass = await _controller.Post(new LodgingModel());

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void TestControllerPut()
    {
      LodgingModel lodgingmodel = (await _unitOfWork.Lodging.SelectAsync(e => e.EntityId == 2)).FirstOrDefault();

      var resultPass = await _controller.Put(lodgingmodel);
      var resultFail = await _controller.Put(null);

      Assert.NotNull(resultPass);
      Assert.NotNull(resultFail);

      LodgingModel lodgingModelBadId = (await _unitOfWork.Lodging.SelectAsync(e => e.EntityId == 2)).FirstOrDefault();
      lodgingModelBadId.EntityId = -1;

      var resultFail2 = await _controller.Put(lodgingModelBadId);

      Assert.NotNull(resultFail2);
    }
  }
}
