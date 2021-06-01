using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace BlogManagement.Core
{
    public static class UtilConvert
    {
        /// <summary>
        /// Object转化为int
        /// </summary>
        /// <param name="thisValue">要转化的值</param>
        /// <returns></returns>
        public static int ObjToInt(this object thisValue)
        {
            int reval = 0;
            if (thisValue == null) return 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }
        /// <summary>
        /// Object转化为int
        /// </summary>
        /// <param name="thisValue">要转化的值</param>
        /// <param name="errorValue">失败后返回值</param>
        /// <returns></returns>
        public static int ObjToInt(this object thisValue, int errorValue)
        {
            int reval = 0;
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        /// <summary>
        /// Object转化为double
        /// </summary>
        /// <param name="thisValue">要转化的值</param>
        /// <returns></returns>
        public static double ObjToDouble(this object thisValue)
        {
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out var reval))
            {
                return reval;
            }
            return 0;
        }
        /// <summary>
        /// Object转化为double
        /// </summary>
        /// <param name="thisValue">要转化的值</param>
        /// <param name="errorValue">失败后返回值</param>
        /// <returns></returns>
        public static double ObjToDouble(this object thisValue, double errorValue)
        {
            if (thisValue != null && thisValue != DBNull.Value && double.TryParse(thisValue.ToString(), out var reval))
            {
                return reval;
            }
            return errorValue;
        }
        /// <summary>
        /// Object转化为string
        /// </summary>
        /// <param name="thisValue">要转化的值</param>
        /// <returns></returns>
        public static string ObjToString(this object thisValue)
        {
            if (thisValue != null) return thisValue.ToString()?.Trim();
            return "";
        }
        /// <summary>
        /// Object类型判断是否为空
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool IsNotEmptyOrNull(this object thisValue)
        {
            return ObjToString(thisValue) != "" && ObjToString(thisValue) != "undefined" && ObjToString(thisValue) != "null";
        }
        /// <summary>
        /// Object转化为string
        /// </summary>
        /// <param name="thisValue">要转化的值</param>
        /// <param name="errorValue">失败后返回值</param>
        /// <returns></returns>
        public static string ObjToString(this object thisValue, string errorValue)
        {
            if (thisValue != null) return thisValue.ToString()?.Trim();
            return errorValue;
        }
        /// <summary>
        /// Object转化为decimal
        /// </summary>
        /// <param name="thisValue">要转化的值</param>
        /// <returns></returns>
        public static Decimal ObjToDecimal(this object thisValue)
        {
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out var reval))
            {
                return reval;
            }
            return 0;
        }
        /// <summary>
        /// Object转化为decimal
        /// </summary>
        /// <param name="thisValue">要转化的值</param>
        /// <param name="errorValue">失败后返回值</param>
        /// <returns></returns>
        public static Decimal ObjToDecimal(this object thisValue, decimal errorValue)
        {
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out var reval))
            {
                return reval;
            }
            return errorValue;
        }
        /// <summary>
        /// Object转化为datetime
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static DateTime ObjToDate(this object thisValue)
        {
            DateTime ravel = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out ravel))
            {
                ravel = Convert.ToDateTime(thisValue);
            }
            return ravel;
        }
        /// <summary>
        /// Object转化为datetime
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static DateTime ObjToDate(this object thisValue, DateTime errorValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return errorValue;
        }
        /// <summary>
        /// Object转化为bool
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool ObjToBool(this object thisValue)
        {
            bool reval = false;
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }

        /// <summary>
        /// 获取当前时间的时间戳
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static string DateToTimeStamp(this DateTime thisValue)
        {
            TimeSpan ts = thisValue - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        public static bool IsImplement(this Type entityType, Type interfaceType)
        {
            return entityType.GetTypeInfo().GetInterfaces().Any(t =>
                t.GetTypeInfo().IsGenericType && t.GetGenericTypeDefinition() == interfaceType);
        }
        /// <summary>
        /// DataTable转dictionary
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static Dictionary<string, object> DataTableToDic(DataTable dt)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            for (int i = 0; i < dt.Columns.Count; i++)
            {

                dic[dt.Columns[i].ColumnName] = Convert.ToString(dt.Rows[0][dt.Columns[i].ColumnName]);
            }
            return dic;
        }

        public static List<Dictionary<string, object>> DataTableToDicList(DataTable dt)
        {
            return dt.AsEnumerable().Select(
                    row => dt.Columns.Cast<DataColumn>().ToDictionary(
                    column => column.ColumnName,
                    column => row[column]
                    )).ToList();
        }
    }
}
