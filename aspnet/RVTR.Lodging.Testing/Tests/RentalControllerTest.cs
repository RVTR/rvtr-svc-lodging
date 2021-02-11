using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Moq;
using RVTR.Lodging.Domain.Interfaces;
using RVTR.Lodging.Domain.Models;
using RVTR.Lodging.Service.Controllers;
using Xunit;

namespace RVTR.Lodging.Testing.Tests
{
  public class RentalControllerTest
  {
    private readonly RentalController _controller;
    private readonly ILogger<RentalController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public RentalControllerTest()
    {
      var loggerMock = new Mock<ILogger<RentalController>>();
      var repositoryMock = new Mock<IRepository<RentalModel>>();
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<RentalModel>())).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync((IEnumerable<RentalModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(-1)).Throws(new KeyNotFoundException());
      repositoryMock.Setup(m => m.SelectAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(1)).ReturnsAsync((RentalModel)null);
      repositoryMock.Setup(m => m.SelectAsync(2)).ReturnsAsync(new RentalModel() { Id = 2, LotNumber = "2", Status = "Available", Price = 1.00 });
      repositoryMock.Setup(m => m.Update(It.IsAny<RentalModel>()));
      unitOfWorkMock.Setup(m => m.Rental).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new RentalController(_logger, _unitOfWork);
    }

    [Fact]
    public async void Test_Controller_Delete()
    {
      var resultFail = await _controller.Delete(-1);
      var resultPass = await _controller.Delete(2);

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Get()
    {
      var resultMany = await _controller.Get();
      var resultFail = await _controller.Get(-1);
      var resultOne = await _controller.Get(2);

      Assert.NotNull(resultMany);
      Assert.NotNull(resultFail);
      Assert.NotNull(resultOne);
    }

    [Fact]
    public async void Test_Controller_Post()
    {
      var resultPass = await _controller.Post(new RentalModel());

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put()
    {
      RentalModel rentalmodel = await _unitOfWork.Rental.SelectAsync(2);

      var resultPass = await _controller.Put(rentalmodel);
      var resultFail = await _controller.Put(null);

      Assert.NotNull(resultPass);
      Assert.NotNull(resultFail);

      RentalModel rentalModelBadId = await _unitOfWork.Rental.SelectAsync(2);
      rentalModelBadId.Id = -1;

      var resultFail2 = await _controller.Put(rentalModelBadId);

      Assert.NotNull(resultFail2);
    }
  }
}
