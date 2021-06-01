using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Core
{
    /// <summary>
    /// 请求授权（调用WEBAPI必须要这些参数）
    /// </summary>
    public class RequestAuthorize
    {
        public static string Token { get; set; }
        public static string GetRequestToken
        {
            get
            {
                var context = HttpContext.Current;
                if (context == null)
                    return "";
                var authorize = context.Request.Headers["Authorization"];
                if (string.IsNullOrEmpty(authorize))
                {
                    authorize = context.Request.Query["Authorization"];
                }
                if (string.IsNullOrEmpty(authorize))
                {
                    try
                    {
                        authorize = context.Request.Form["Authorization"];
                    }
                    catch (Exception)
                    {
                        authorize = string.Empty;
                    }
                }
                if (string.IsNullOrEmpty(authorize))
                {
                    authorize = Token;
                }
                return authorize;
            }
        }
    }
}
