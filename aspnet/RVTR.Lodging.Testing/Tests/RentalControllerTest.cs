using System;
using System.Collections.Generic;
using System.Linq;
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
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == -1)).Throws(new KeyNotFoundException());
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == 0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == 1)).ReturnsAsync((IEnumerable<RentalModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == 2)).ReturnsAsync(new[] { new RentalModel() { EntityId = 2, LotNumber = "2", Status = "Available", Price = 1.00 } });
      repositoryMock.Setup(m => m.Update(It.IsAny<RentalModel>()));
      unitOfWorkMock.Setup(m => m.Rental).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new RentalController(_logger, _unitOfWork);
    }

    [Fact]
    public async void TestControllerDelete()
    {
      var resultFail = await _controller.Delete(-1);
      var resultPass = await _controller.Delete(2);

      Assert.NotNull(resultFail);
      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void TestControllerGet()
    {
      var resultMany = await _controller.Get();
      var resultFail = await _controller.Get(-1);
      var resultOne = await _controller.Get(2);

      Assert.NotNull(resultMany);
      Assert.NotNull(resultFail);
      Assert.NotNull(resultOne);
    }

    [Fact]
    public async void TestControllerPost()
    {
      var resultPass = await _controller.Post(new RentalModel());

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void TestControllerPut()
    {
      RentalModel rentalmodel = (await _unitOfWork.Rental.SelectAsync(e => e.EntityId == 2)).FirstOrDefault();

      var resultPass = await _controller.Put(rentalmodel);
      var resultFail = await _controller.Put(null);

      Assert.NotNull(resultPass);
      Assert.NotNull(resultFail);

      RentalModel rentalModelBadId = (await _unitOfWork.Rental.SelectAsync(e => e.EntityId == 2)).FirstOrDefault();
      rentalModelBadId.EntityId = -1;

      var resultFail2 = await _controller.Put(rentalModelBadId);

      Assert.NotNull(resultFail2);
    }
  }
}
