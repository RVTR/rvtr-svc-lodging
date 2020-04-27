using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.WebApi.Controllers
{
  /// <summary>
  /// API controller for location model interaction with storage domain
  /// </summary>
  [ApiController]
  [EnableCors()]
  [Route("[controller]/[action]")]
  public class LocationController : ControllerBase
  {
    private readonly ILogger<LodgingController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public LocationController(ILogger<LodgingController> logger, IUnitOfWork unitOfWork)
    {
      _logger = logger;
      _unitOfWork = unitOfWork;
    }


    /// <summary>
    /// Get method for all locationss
    /// </summary>
    /// <returns>List of locations</returns>
    [HttpGet]
    public async Task<IEnumerable<LocationModel>> Get()
    {
      return await Task.FromResult<IEnumerable<LocationModel>>(_unitOfWork.LocationRepository.Select());
    }

    /// <summary>
    /// Get method for specific location
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Single Duration</returns>
    [HttpGet("{id}")]
    public async Task<LocationModel> Get(int id)
    {
      return await Task.FromResult<LocationModel>(_unitOfWork.LocationRepository.Select(id));
    }

    /// <summary>
    /// /// Post method for location
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Returns an action result describing the post action</returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody]LocationModel model)
    {
      var success = await Task.FromResult<bool>(_unitOfWork.LocationRepository.Insert(model));
      if (success)
      {
        return Ok();
      }
      return BadRequest();
    }

    /// <summary>
    /// Put method for location
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Request success or failure</returns>
    [HttpPut]
    public async Task<IActionResult> Put([FromBody]LocationModel model)
    {
      var success = await Task.FromResult<bool>(_unitOfWork.LocationRepository.Update(model));
            if (success)
      {
        return Ok();
      }
      return BadRequest();
    }

    /// <summary>
    /// Delete method for locations
    /// </summary>
    /// <param name="model"></param>
    /// <returns>Request success or failure</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int model)
    {
      var success = await Task.FromResult<bool>(_unitOfWork.LocationRepository.Delete(model));
      if (success)
      {
        return Ok();
      }
      return BadRequest();
    }
  }
}
