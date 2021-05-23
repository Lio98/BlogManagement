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
using BlogManagement.Dal;
using BlogManagement.Interface;
using BlogManagement.Model;
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
            var isExist = _user.UserLogin(account, password);
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
        /// 注册用户
        /// </summary>
        /// <param name="sUserInfo"></param>
        /// <returns></returns>
        [HttpPost("AddUserInfo")]
        public IActionResult AddUserInfo(string sUserInfo)
        {
            T_Sys_User userInfo = JsonConvert.DeserializeObject<T_Sys_User>(sUserInfo);
            if (userInfo == null)
            {
                return new JsonResult(JsonConvert.SerializeObject(new
                {
                    StatusCode=200,
                    Status=ReturnStatus.Fail,
                    Msg="传入数据格式不正确，请检查数据是否正确"
                }));
            }

            if (_user.AddUserInfo(userInfo))
            {
                return new JsonResult(JsonConvert.SerializeObject(new
                {
                    StatusCode = 200,
                    Status = ReturnStatus.Success,
                    Msg = "添加成功"
                }));
            }
            return new JsonResult(JsonConvert.SerializeObject(new
            {
                StatusCode = 200,
                Status = ReturnStatus.Fail,
                Msg = "添加用户信息失败，请联系系统管理员"
            }));
        }

        [HttpGet("TestException")]
        [AllowAnonymous]
        public IActionResult TestException()
        {
            throw new Exception("出现错误了");
        }
    }
}
