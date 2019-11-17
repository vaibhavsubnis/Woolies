using Microsoft.AspNetCore.Mvc;
using OnlineShopping.Services;

namespace OnlineShopping.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private IUserService _userService;

        public AnswersController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("user")]
        public IActionResult GetUser()
        {
            return Ok(_userService.GetUser());
        }
    }
}