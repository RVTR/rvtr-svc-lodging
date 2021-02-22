using System.Collections.Generic;
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
      var repositoryMock = new Mock<ILodgingRepo>();
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      repositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync((IEnumerable<LodgingModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(-1)).Throws(new KeyNotFoundException());
      repositoryMock.Setup(m => m.SelectAsync(0)).ReturnsAsync(new LodgingModel());
      repositoryMock.Setup(m => m.SelectAsync(1)).ReturnsAsync((LodgingModel)null);
      repositoryMock.Setup(m => m.SelectAsync(2)).ReturnsAsync(new LodgingModel() { Id = 2, AddressId = 2, Name = "name", Bathrooms = 1 });
      unitOfWorkMock.Setup(m => m.Lodging).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new LodgingController(_logger, _unitOfWork);
    }

    [Fact]
    public async void Test_Controller_Get()
    {
      var resultMany = await _controller.Get();

      Assert.NotNull(resultMany);
    }

    [Fact]
    public async void Test_Controller_GetID()
    {
      var failResult = await _controller.Get(-1);
      var returnOneResult = await _controller.Get(2);

      Assert.NotNull(failResult);
      Assert.NotNull(returnOneResult);
    }

    [Fact]
    public async void Test_Controller_Delete()
    {
      var resultPass = await _controller.Delete(-1);
      var resultFail = await _controller.Delete(2);

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Post()
    {
      var resultPass = await _controller.Post(new LodgingModel());

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put()
    {
      LodgingModel lodgingmodel = await _unitOfWork.Lodging.SelectAsync(2);

      var resultPass = await _controller.Put(lodgingmodel);
      var resultFail = await _controller.Put(null);

      Assert.NotNull(resultPass);
      Assert.NotNull(resultFail);

      LodgingModel lodgingModelBadId = await _unitOfWork.Lodging.SelectAsync(2);
      lodgingModelBadId.Id = -1;

      var resultFail2 = await _controller.Put(lodgingModelBadId);

      Assert.NotNull(resultFail2);
    }
  }
}
