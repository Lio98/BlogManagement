using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Core
{
    /// <summary>
    /// 异常抓取
    /// </summary>
    [Serializable]
    public class ExceptionEx : Exception
    {
        /// <summary>
        /// 内部异常实例化一个
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public ExceptionEx(string message, Exception inner)
            : base(message, inner) { }
        /// <summary>
        /// 自定义异常
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static ExceptionEx Throw(Exception e)
        {
            return new ExceptionEx("请求出错，稍后重试", e);
        }
    }
}
