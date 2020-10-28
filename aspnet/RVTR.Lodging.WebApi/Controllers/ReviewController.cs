using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.ObjectModel.Interfaces;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.WebApi.Controllers
{
  /// <summary>
  ///
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
    ///
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public ReviewController(ILogger<ReviewController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
      try
      {
        await _unitOfWork.Review.DeleteAsync(id);
        await _unitOfWork.CommitAsync();

        return Ok();
      }
      catch
      {
        return NotFound(id);
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      return Ok(await _unitOfWork.Review.SelectAsync());
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
      try
      {
        return Ok(await _unitOfWork.Review.SelectAsync(id));
      }
      catch
      {
        return NotFound(id);
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="review"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(ReviewModel review)
    {
      //Checks to see if review obj is valid. Returns a bad request if invalid, otherwise inserts it into the db.
      var validationResults = review.Validate(new ValidationContext(review));
      if (validationResults != null || validationResults.Count() > 0)
      {
        return BadRequest(review);
      }
      else
      {
        await _unitOfWork.Review.InsertAsync(review);
        await _unitOfWork.CommitAsync();

        return Accepted(review);
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="review"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(ReviewModel review)
    {
      //Checks to see if review obj is valid. Returns a bad request if invalid, otherwise inserts it into the db.
      var validationResults = review.Validate(new ValidationContext(review));
      if (validationResults != null || validationResults.Count() > 0)
      {
        return BadRequest(review);
      }
      else
      {
        try
        {
          _unitOfWork.Review.Update(review);
          await _unitOfWork.CommitAsync();

          return Accepted(review);
        }
        catch
        {
          return NotFound(review);
        }
      }
    }
  }
}
