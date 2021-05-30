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
using BlogManagement.Core;
using BlogManagement.Dal;
using BlogManagement.Dal.Dal;
using BlogManagement.Interface;
using BlogManagement.Model;
using BlogManagement.Model.Model;
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
        private JWTTokenOptions _jwtTokenOptions = null;
        private IUser _user = null;

        public LoginController(JWTTokenOptions jwtTokenOptions,IUser user) 
        {
            this._jwtTokenOptions = jwtTokenOptions;
            this._user = user;
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
            var isExist = _user.UserLogin(account, password,out var userInfo);
            //用户不存在
            if (!isExist)
            {
                return new JsonResult(JsonConvert.SerializeObject(new
                {
                    StatusCode=200,
                    Status=ReturnStatus.Fail,
                    Msg="用户名或密码错误，请重新输入"
                }));
            }

            var token = JWTTokenHelper.JwtEncrypt(new TokenModelJwt() { UserId=userInfo.Id,Level=""} ,this._jwtTokenOptions);
            using (RedisStringService service=new RedisStringService())
            {
                service.Set<T_Sys_User>("Bearer "+token, userInfo);
            }

            

            return new JsonResult(new ReturnResultModel()
            {
                StatusCode = 200,
                Status = ReturnStatus.Success,
                Data= token,
                Msg = "登录成功"
            });
        }
        
    }
}
