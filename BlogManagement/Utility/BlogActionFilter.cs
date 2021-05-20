using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Serilog.Core;

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
            _logger.LogInformation("start");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("end");
        }
    }
}
