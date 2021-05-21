using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Newtonsoft.Json;

namespace BlogManagement.Utility
{
    public class BlogActionFilter : IActionFilter
    {
        private ILogger<BlogActionFilter> _logger = null;

        public BlogActionFilter(ILogger<BlogActionFilter> logger)
        {
            this._logger = logger;
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpRequest = context.HttpContext.Request;
            _logger.LogInformation($"请求调用开始，请求方式是：{httpRequest.Method}, 请求路径是：{httpRequest.Path}, 请求参数是：{(context.ActionArguments!=null?JsonConvert.SerializeObject(context.ActionArguments) :null)}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var httpResponse = context.HttpContext.Response;
            _logger.LogInformation($"请求调用结束, 状态码是：{httpResponse.StatusCode}, 返回结果是：{(context.Result != null ? JsonConvert.SerializeObject(context.Result) : null)}");
        }
    }
}
