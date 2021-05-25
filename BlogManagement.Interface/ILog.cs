using System;
using System.Collections.Generic;
using System.Text;
using BlogManagement.Model;

namespace BlogManagement.Interface
{
    public interface ILog
    {
        /// <summary>
        /// 写入系统日志
        /// </summary>
        /// <param name="logInfo"></param>
        /// <returns></returns>
        bool AddLogsInfo(T_Sys_Logs logInfo);
    }
}
