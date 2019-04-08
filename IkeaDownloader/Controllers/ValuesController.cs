using System.Threading.Tasks;
using IkeaDownloader.Ports;
using Microsoft.AspNetCore.Mvc;

namespace IkeaDownloader.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ValuesController : ControllerBase
  {
    private readonly IGetHandler _getHandler;

    public ValuesController(IGetHandler getHandler)
    {
      _getHandler = getHandler;
    }

    // GET api/values
    [HttpGet]
    public async Task<IActionResult> Get()
    {
      return Ok(await _getHandler.Handle());
    }
  }
}