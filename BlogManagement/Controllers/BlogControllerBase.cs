using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlogManagement.Dal.Dal;
using BlogManagement.Interface;
using BlogManagement.Model;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.Controllers
{
    public class BlogControllerBase : ControllerBase
    {
        protected static T_Sys_User logInUserInfo = new T_Sys_User();

        /// <summary>
        /// 写入用户操作日志
        /// </summary>
        /// <param name="logInfo"></param>
        protected void AddOperationLogs(T_Sys_Logs logInfo)
        {
            logInfo.CreateTime=DateTime.Now;
            ILog log = new LogDal();
            log.AddLogsInfo(logInfo);
        }
    }
}
