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
    public class ImageController: ControllerBase
    {

    }
}