using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BasicResponse.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BasicResponse.Controllers
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