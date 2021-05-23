using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManagement.Model.Enum;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace BlogManagement.Utility
{
    public class BlogExceptionFilter : IExceptionFilter
    {
        private ILogger<BlogExceptionFilter> _logger = null;

        public BlogExceptionFilter(ILogger<BlogExceptionFilter> logger)
        {
            this._logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            var httpContext = context.HttpContext;
            if (!context.ExceptionHandled)
            {
                this._logger.LogError($"在响应{httpContext.Request.Path}时出现异常，异常信息：{context.Exception.Message}\r\n{context.Exception}");
                context.Result = new JsonResult(new
                {
                    StatusCode = 500,
                    DeBugMessage = context.Exception.Message,
                    Status = ReturnStatus.Fail,
                    Msg = "发生错误，请联系管理员"
                });

                context.ExceptionHandled = true;
            }
        }
    }
}
