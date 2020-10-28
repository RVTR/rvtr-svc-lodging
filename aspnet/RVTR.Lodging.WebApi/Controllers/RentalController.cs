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
  public class RentalController : ControllerBase
  {
    private readonly ILogger<RentalController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    /// <summary>
    ///
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="unitOfWork"></param>
    public RentalController(ILogger<RentalController> logger, IUnitOfWork unitOfWork)
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
        await _unitOfWork.Rental.DeleteAsync(id);
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
      return Ok(await _unitOfWork.Rental.SelectAsync());
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
        return Ok(await _unitOfWork.Rental.SelectAsync(id));
      }
      catch
      {
        return NotFound(id);
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="rental"></param>
    /// <returns></returns>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(RentalModel rental)
    {
      //Checks to see if rental obj is valid. Returns a bad request if invalid, otherwise inserts it into the db.
      var validationResults = rental.Validate(new ValidationContext(rental));
      if (validationResults != null || validationResults.Count() > 0) //If rental obj is invalid...
      {
        return BadRequest(rental);                                    //Return bad request
      }
      else
      {
        await _unitOfWork.Rental.InsertAsync(rental);
        await _unitOfWork.CommitAsync();

        return Accepted(rental);
      }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="rental"></param>
    /// <returns></returns>
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status202Accepted)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Put(RentalModel rental)
    {
      //Checks to see if rental obj is valid. Returns a bad request if invalid, otherwise updates the db entry.
      var validationResults = rental.Validate(new ValidationContext(rental));
      if (validationResults != null || validationResults.Count() > 0)
      {
        return BadRequest(rental);           //Returns bad request if invalid input given
      }
      else
      {
        try
        {
          _unitOfWork.Rental.Update(rental); //Updates the entry
          await _unitOfWork.CommitAsync();   //Saves changes to the context

          return Accepted(rental);           //Returns 202 ok code
        }
        catch
        {
          return NotFound(rental);          //Returns 404 if entry not found in db
        }
      }
    }
  }
}
