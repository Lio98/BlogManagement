using BlogManagement.Model.Enum;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILogger<LoginController> _logger = null;
        public LoginController(ILogger<LoginController> logger) 
        {
            this._logger = logger;
        }

        [HttpGet("login")]
        public IActionResult Login(string account, string password) 
        {
            _logger.LogInformation($"访问的控制器为{base.Request.RouteValues["controller"]},方法名称为{base.Request.RouteValues["action"]}");
            return new JsonResult(JsonConvert.SerializeObject(new
            {
                StatusCode = 200,
                Status = ReturnStatus.Success,
                Msg = "登录成功"
            }));
        }
    }
}
