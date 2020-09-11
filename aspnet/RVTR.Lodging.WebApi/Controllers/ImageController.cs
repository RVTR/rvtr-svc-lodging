using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RVTR.Lodging.DataContext.Repositories;
using RVTR.Lodging.ObjectModel.Models;

namespace RVTR.Lodging.WebApi.Controllers
{
    [APIController]
    [EnableCors("public")]
    [ApiVersion("0.0")]
    [Route("rest/lodging/{version:apiVersion}/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ILogger<ImageController> _logger;
        private readonly UnitOfWork _unitOfWork;

        /// <summary>
        /// Constructor for the ImageController sets up logger and unitOfWork dependencies
        /// </summary>
        /// <param name="logger">The Logger</param>
        /// <param name="unitOfWork">The UnitOfWork</param>
        public ImageController(ILogger<LodgingController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets all the images in the blob storage
        /// </summary>
        /// <returns>The Image</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
        {
            return Ok();
        }

    }
}