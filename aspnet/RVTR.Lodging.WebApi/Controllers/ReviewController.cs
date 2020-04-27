using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.WebApi.Controllers
{

  /// <summary>
  /// Api controller for interacting with reviews
  /// </summary>
  /// <returns>List of Reviews</returns>
  [ApiController]
  [EnableCors()]
  [Route("[controller]/[action]")]
  public class ReviewController : ControllerBase
  {
    private readonly ILogger<ReviewController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public ReviewController(ILogger<ReviewController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Get method for all Reviews
    /// </summary>
    /// <returns>List of Review</returns>
    [HttpGet]
    public async Task<IEnumerable<ReviewModel>> Get()
    {
      return await Task.FromResult<IEnumerable<ReviewModel>>(_unitOfWork.ReviewRepository.Select());
    }

    /// <summary>
    /// Get method for specific review
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Single Review</returns>
    [HttpGet("{id}")]
    public async Task<ReviewModel> GetOne(int id)
    {
      return await Task.FromResult<ReviewModel>(_unitOfWork.ReviewRepository.Select(id));
    }

    /// <summary>
    /// Post method for Review
    /// </summary>
    /// <param name="Review"></param>
    /// <returns>Returns an action result describing the post action</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]ReviewModel review)
    {
      var success = await Task.FromResult<bool>(_unitOfWork.ReviewRepository.Insert(review));
      if(success)
      {
        return Ok();
      }
      return BadRequest();
    }

    /// <summary>
    /// Put method for review
    /// </summary>
    /// <param name="review"></param>
    /// <returns>Request success or failure</returns>
    [HttpPut]
    public async Task<IActionResult> Put([FromBody]ReviewModel review)
    {
      var success = await Task.FromResult<bool>(_unitOfWork.ReviewRepository.Update(review));
      if(success)
      {
        return Ok();
      }
      return BadRequest();
    }

    /// <summary>
    /// Delete method for review
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Request success or failure</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      var success = await Task.FromResult<bool>(_unitOfWork.ReviewRepository.Delete(id));
      if(success)
      {
        return Ok();
      }
      return BadRequest();
    }
  }
}
