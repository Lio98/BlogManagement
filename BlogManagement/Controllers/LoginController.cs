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
    /// <summary>
    /// 登录
    /// </summary>
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
        
        /// <summary>
        /// 用户登陆
        /// </summary>
        /// <param name="account">用户名</param>
        /// <param name="password">密码</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet("login")]
        public IActionResult Login(string account, string password) 
        {
            var token = JWTTokenHelper.JwtEncrypt(new TokenModelJwt() { UserId=123,Level=""} ,this._jwtTokenOptions);
            return new JsonResult(JsonConvert.SerializeObject(new
            {
                StatusCode = 200,
                Status = ReturnStatus.Success,
                Token= token,
                Msg = "登录成功"
            }));
        }

        /// <summary>
        /// 测试
        /// </summary>
        [HttpGet("Test")]
        public IActionResult Test() 
        {
            return Ok("success");
        }
    }
}
