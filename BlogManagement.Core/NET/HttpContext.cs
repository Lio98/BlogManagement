using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Core
{
    public static class HttpContext
    {
        private static IHttpContextAccessor _httpContextAccessor;
        public static void Configure(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public static Microsoft.AspNetCore.Http.HttpContext Current
        {
            get
            {
                return _httpContextAccessor.HttpContext;
            }
        }
    }
}
