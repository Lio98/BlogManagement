using System;
using System.Collections.Generic;
using System.Text;
using BlogManagement.Interface;
using BlogManagement.Model;

namespace BlogManagement.Dal.Dal
{
    public class LogDal : BaseDal, ILog
    {
        /// <summary>
        /// 写入用户操作日志
        /// </summary>
        /// <param name="logs"></param>
        /// <returns></returns>
        public bool AddLogsInfo(T_Sys_Logs logInfo)
        {
            return this.Db.Insert<T_Sys_Logs>(logInfo).ExecuteAffrows() > 0 ? true : false;
        }
    }
}
