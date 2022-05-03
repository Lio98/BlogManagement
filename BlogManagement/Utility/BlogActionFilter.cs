using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManagement.Core;
using BlogManagement.Interface;
using BlogManagement.Model;
using BlogManagement.Model.Enum;
using BlogManagement.Model.Model;
using BlogManagement.Model.Model.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog.Core;
using Newtonsoft.Json;

namespace BlogManagement.Utility
{
    public class BlogActionFilter : IActionFilter
    {
        private ILogger<BlogActionFilter> _logger = null;
        private ILog _logServer = null;

        public BlogActionFilter(ILogger<BlogActionFilter> logger, ILog logServer)
        {
            this._logger = logger;
            this._logServer = logServer;
        }

        private static string _requestValue = null;

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var httpRequest = context.HttpContext.Request;
            _requestValue = (context.ActionArguments != null
                ? JsonConvert.SerializeObject(context.ActionArguments)
                : null);
            _logger.LogInformation($"开始调用 {httpRequest.Path}接口, 请求方式是：{httpRequest.Method}, 请求参数是：{_requestValue}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var httpResponse = context.HttpContext.Response;
            var returnResult = context.Result as JsonResult;
            var responseValue = (returnResult != null ? JsonConvert.SerializeObject(returnResult.Value) : null);
            _logger.LogInformation($"调用 {context.HttpContext.Request.Path}接口结束, 状态码是：{httpResponse.StatusCode}, 执行结果是：{responseValue}");

            OperationType operation = OperationType.搜索;
            OperationLogType operationLogType = OperationLogType.操作日志;

            

            
            switch (context.HttpContext.Request.Method.ToUpper())
            {
                case "GET":
                    operation = OperationType.搜索;
                    break;
                case "POST":
                    operation = OperationType.新增;
                    break;
                case "PUT":
                    operation = OperationType.修改;
                    break;
                case "DELETE":
                    operation = OperationType.删除;
                    break;
                default:
                    break;
            }

            var userProvider = new UserProvider();
            var userInfo = userProvider.Get();
            string content = string.Empty;
            if (context.HttpContext.Request.Path.Equals("/api/Login/Login"))
            {
                operation = OperationType.登录;
                operationLogType = OperationLogType.登录日志;
                if (returnResult != null)
                {
                    string token = (returnResult.Value as ReturnResultModel)?.Data.ToString();
                    if (token.IsNullOrEmpty()) return;
                    userInfo = userProvider.Get("Bearer " + token);
                    content = $"用户[{userInfo?.Account}] {operation}系统, 输入的账号是 {userInfo?.Account},返回的数据是 {responseValue}";
                }
            }
            else if (context.HttpContext.Request.Path.Equals("/api/Login/Logout"))
            {
                operation = OperationType.退出;
                operationLogType = OperationLogType.登录日志;
                content = $"用户[{userInfo?.Account}] {operation}系统, 退出的账号是 {userInfo?.Account},返回的数据是 {responseValue}";
            }

            if (userInfo != null)
            {
                var entity = new T_Sys_Logs()
                {
                    Operation = operation.EnumIntToString(),
                    Operator = userInfo.Id,
                    Content = content.IsNullOrEmpty()
                        ? $"用户[{userInfo.Account}] {operation}数据, 输入的数据是 {_requestValue},返回的数据是 {responseValue}"
                        : content,
                    ResponseCode = httpResponse.StatusCode,
                    Type = operationLogType.EnumIntToString()
                };
                _logServer.AddLogsInfo(entity);
            }
        }
    }
}
