using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Model.Enum
{
    /// <summary>
    /// 系统日志类型
    /// </summary>
    public enum SysLogType : int
    {
        全部 = 0,

        登录日志 = 1,

        系统异常 = 2,

        操作日志 = 3
    }
}
