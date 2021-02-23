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
  public class ReviewControllerTest
  {
    private readonly ReviewController _controller;
    private readonly ILogger<ReviewController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ReviewControllerTest()
    {
      var loggerMock = new Mock<ILogger<ReviewController>>();
      var repositoryMock = new Mock<IRepository<ReviewModel>>();
      var unitOfWorkMock = new Mock<IUnitOfWork>();

      repositoryMock.Setup(m => m.DeleteAsync(0)).Throws(new Exception());
      repositoryMock.Setup(m => m.DeleteAsync(1)).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.InsertAsync(It.IsAny<ReviewModel>())).Returns(Task.CompletedTask);
      repositoryMock.Setup(m => m.SelectAsync()).ReturnsAsync((IEnumerable<ReviewModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == -1)).Throws(new KeyNotFoundException());
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == 0)).Throws(new Exception());
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == 1)).ReturnsAsync((IEnumerable<ReviewModel>)null);
      repositoryMock.Setup(m => m.SelectAsync(e => e.EntityId == 2)).ReturnsAsync(new []  { new ReviewModel() { EntityId = 2, Comment = "Random", DateCreated = DateTime.Now, Rating = 1 }});
      repositoryMock.Setup(m => m.Update(It.IsAny<ReviewModel>()));
      unitOfWorkMock.Setup(m => m.Review).Returns(repositoryMock.Object);

      _logger = loggerMock.Object;
      _unitOfWork = unitOfWorkMock.Object;
      _controller = new ReviewController(_logger, _unitOfWork);
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
      var resultPass = await _controller.Post(new ReviewModel());

      Assert.NotNull(resultPass);
    }

    [Fact]
    public async void Test_Controller_Put()
    {
      ReviewModel reviewmodel = (await _unitOfWork.Review.SelectAsync(e => e.EntityId == 2)).FirstOrDefault();

      var resultPass = await _controller.Put(reviewmodel);
      var resultFail = await _controller.Put(null);

      Assert.NotNull(resultPass);
      Assert.NotNull(resultFail);

      ReviewModel reviewModelBadId = (await _unitOfWork.Review.SelectAsync(e => e.EntityId == 2)).FirstOrDefault();
      reviewModelBadId.EntityId = -1;

      var resultFail2 = await _controller.Put(reviewModelBadId);

      Assert.NotNull(resultFail2);
    }
  }
}
