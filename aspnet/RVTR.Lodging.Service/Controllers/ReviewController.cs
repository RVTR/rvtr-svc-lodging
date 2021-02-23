using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.Domain.Interfaces;
using RVTR.Lodging.Domain.Models;

namespace RVTR.Lodging.Service.Controllers
{
  /// <summary>
  /// Review controller
  /// </summary>
  [ApiController]
  [ApiVersion("0.0")]
  [EnableCors("public")]
  [Route("rest/lodging/{version:apiVersion}/[controller]")]
  public class ReviewController : ControllerBase
  {
    private readonly ILogger<ReviewController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    /// Constructor of the review controller
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public ReviewController(ILogger<ReviewController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    /// Action method for deleting a review by review id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        _logger.LogInformation($"Deleting a review @ id = {id}...");
        var review = (await _unitOfWork.Review.SelectAsync(e => e.EntityId == id)).FirstOrDefault();
        
        await _unitOfWork.Review.DeleteAsync(review.EntityId);
        await _unitOfWork.CommitAsync();
        _logger.LogInformation($"Successfully deleted a review @ id = {review.EntityId}.");
        return Ok();
      }
      catch (KeyNotFoundException e)
      {
        _logger.LogInformation(e, "Caught: {e.Message}. Id = {id}.", e, id);
        return NotFound(id);
      }
    }

    /// <summary>
    /// Action method for getting all reviews
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      _logger.LogInformation($"Getting all reviews...");
      return Ok(await _unitOfWork.Review.SelectAsync());
    }

    /// <summary>
    /// Action method for getting a review by review id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        _logger.LogInformation($"Getting a review @ id = {id}...");
        var review = await _unitOfWork.Review.SelectAsync(e => e.EntityId == id);
        return Ok(review);
      }
      catch (KeyNotFoundException e)
      {
        _logger.LogInformation(e, "Caught: {e.Message}. Id = {id}.", e, id);
        return NotFound(id);
      }
    }

    /// <summary>
    /// Action method for creating a new review
    /// </summary>
    /// <param name="review"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post(ReviewModel review)
    {
      _logger.LogInformation($"Creating a new review @ {review}...");
      await _unitOfWork.Review.InsertAsync(review);
      await _unitOfWork.CommitAsync();
      _logger.LogInformation($"Successfully created a new review @ {review}.");
      return Accepted(review);
    }

    /// <summary>
    /// Action method for updating a preexisting review
    /// </summary>
    /// <param name="review"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(ReviewModel review)
    {
      try
      {
        _logger.LogInformation($"Updating a review @ {review}...");
        var selectedReview = (await _unitOfWork.Review.SelectAsync(e => e.EntityId == review.EntityId)).FirstOrDefault();

        _unitOfWork.Review.Update(selectedReview);
        await _unitOfWork.CommitAsync();
        _logger.LogInformation($"Successfully updated a review @ {selectedReview}.");
        return Accepted(selectedReview);
      }
      catch (NullReferenceException e)
      {
        _logger.LogInformation(e, "Caught: {e}. Given review parameter was null.", e);
        return NotFound(review);
      }
      catch (KeyNotFoundException e)
      {
        _logger.LogInformation(e, "Caught: {e.Message}. Id = {review.Id}", e, review);
        return NotFound(review.EntityId);
      }
    }
  }
}
