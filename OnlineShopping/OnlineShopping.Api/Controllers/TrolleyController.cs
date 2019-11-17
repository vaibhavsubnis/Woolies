using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Entities;
using OnlineShopping.Services;

namespace OnlineShopping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrolleyController : ControllerBase
    {
        private ITrolleyService _trolleyService;

        public TrolleyController(ITrolleyService trolleyService)
        {
            _trolleyService = trolleyService;
        }

        [HttpPost]
        [Route("trolleyTotal")]
        public async Task<IActionResult> TrolleyTotal(TrolleyTotalRequest request)
        {
            return Ok(await _trolleyService.GetLowestTrolleyTotal(request));
        }
    }
}