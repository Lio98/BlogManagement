using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManagement.Core;
using BlogManagement.Interface;
using BlogManagement.Model;
using BlogManagement.Model.Enum;
using BlogManagement.Model.Model;
using BlogManagement.Utility;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace BlogManagement.Controllers
{
    /// <summary>
    /// 用户信息
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    [ServiceFilter(typeof(BlogActionFilter))]
    public class UserController : ControllerBase
    {
        private IUser _user = null;

        public UserController(IUser user)
        {
            this._user = user;
        }

        /// <summary>
        /// 根据Id查询用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("GetUserInfoById")]
        public IActionResult GetUserInfoById(long id)
        {
            if (id == 0)
            {
                return new JsonResult(new ReturnResultModel()
                {
                    StatusCode = 200,
                    Status = ReturnStatus.Fail,
                    Msg = "传入的Id不可为空"
                });
            }

            T_Sys_User userInfo = _user.GetUserInfoById(id);
            if (userInfo == null)
            {
                return new JsonResult(new ReturnResultModel()
                {
                    StatusCode = 200,
                    Status = ReturnStatus.Success,
                    Msg = "查询结果为空"
                });
            }
            return new JsonResult(JsonConvert.SerializeObject(new ReturnResultModel()
            {
                StatusCode = 200,
                Status = ReturnStatus.Success,
                Data = userInfo,
                Msg = ""
            }));
        }

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="sUserInfo"></param>
        /// <returns></returns>
        [HttpPost("AddUserInfo")]
        public IActionResult AddUserInfo(string sUserInfo)
        {
            T_Sys_User userInfo = JsonConvert.DeserializeObject<T_Sys_User>(sUserInfo);
            if (userInfo == null)
            {
                return new JsonResult(new ReturnResultModel()
                {
                    StatusCode = 200,
                    Status = ReturnStatus.Fail,
                    Msg = "传入数据格式不正确，请检查数据是否正确"
                });
            }

            if (_user.AddUserInfo(userInfo, out string msg))
            {
                return new JsonResult(new ReturnResultModel()
                {
                    StatusCode = 200,
                    Status = ReturnStatus.Success,
                    Msg = "添加成功"
                });
            }
            return new JsonResult(new ReturnResultModel()
            {
                StatusCode = 200,
                Status = ReturnStatus.Fail,
                Msg = msg
            });
        }


    }
}
