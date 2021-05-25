using System;
using System.Collections.Generic;
using System.Text;
using ServiceStack;

namespace BlogManagement.Core
{
    public static class EnumHelper
    {
        /// <summary>
        /// 将值为int类型的枚举的结果转化为string类型
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string EnumIntToString(this Enum values)
        {
            return (values.ConvertTo<int>()).ToString();
        }
    }
}
