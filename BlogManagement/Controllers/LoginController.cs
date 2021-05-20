using BlogManagement.Model.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BlogManagement.Utility;
using BlogManagement.Utility.JWT;
using Microsoft.AspNetCore.Authorization;

namespace BlogManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(BlogActionFilter))]
    public class LoginController : ControllerBase
    {
        private ILogger<LoginController> _logger = null;
        private JWTTokenOptions _jwtTokenOptions = null;
        public LoginController(ILogger<LoginController> logger,JWTTokenOptions jwtTokenOptions) 
        {
            this._logger = logger;
            this._jwtTokenOptions = jwtTokenOptions;
        }
        
        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string account, string password) 
        {
            var info = JWTTokenHelper.AuthorizeToken(123, "XMN", this._jwtTokenOptions);
            return new JsonResult(JsonConvert.SerializeObject(new
            {
                StatusCode = 200,
                Status = ReturnStatus.Success,
                Data=JsonConvert.SerializeObject(info),
                Msg = "登录成功"
            }));
        }
    }
}
