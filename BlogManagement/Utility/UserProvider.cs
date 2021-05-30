using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManagement.Core;
using BlogManagement.Model;

namespace BlogManagement.Utility
{
    public class UserProvider
    {
        /// <summary>
        /// 静态实例
        /// </summary>
        public static UserProvider Instance => new UserProvider();

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="token">认证信息</param>
        /// <returns></returns>
        public T_Sys_User Get(string token = null)
        {
            var _token = token ?? RequestAuthorize.GetRequestToken;
            if (_token != null)
            {
                using RedisStringService service=new RedisStringService();
                return service.Get<T_Sys_User>(_token);
            }
            else
                return null;
        }

        /// <summary>
        /// 是否过期
        /// </summary>
        /// <returns></returns>
        public bool IsOverdue => this.Get() != null;
    }
}
