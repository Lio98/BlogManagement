using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace BlogManagement.Core
{
    public static class ExtTable
    {
        /// <summary>
        /// 获取表里某页的数据
        /// </summary>
        /// <param name="data">表数据</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="allPage">返回总页数</param>
        /// <returns>返回当页表数据</returns>
        public static DataTable GetPage(this DataTable data, int pageIndex, int pageSize, out int allPage)
        {
            allPage = data.Rows.Count / pageSize;
            allPage += data.Rows.Count % pageSize == 0 ? 0 : 1;
            DataTable table = data.Clone();
            int startIndex = pageIndex * pageSize;
            int endIndex = startIndex + pageSize > data.Rows.Count ? data.Rows.Count : startIndex + pageSize;
            if (startIndex >= endIndex) return table;
            for (var i = startIndex; i < endIndex; i++)
            {
                table.ImportRow(data.Rows[i]);
            }
            return table;
        }

        /// <summary>
        /// 根据字段过滤表的内容
        /// </summary>
        /// <param name="data">表数据</param>
        /// <param name="condition">条件</param>
        /// <returns></returns>
        /// 
        public static DataTable GetDataFilter(DataTable data, string condition)
        {
            if (data != null && data.Rows.Count > 0)
            {
                if (condition.Trim() == "")
                {
                    return data;
                }
                else
                {
                    var newDt = data.Clone();
                    DataRow[] dr = data.Select(condition);
                    foreach (var item in dr)
                    {
                        newDt.ImportRow((DataRow)item);
                    }
                    return newDt;
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取表里前几条数据
        /// </summary>
        /// <param name="data">表数据</param>
        /// <param name="top"></param>
        /// <returns></returns>
        public static DataTable GetSelectTop(DataTable data, int top)
        {
            if (data.Rows.Count < top) return data;
            DataTable newTable = data.Clone();
            DataRow[] rows = data.Select("1=1");
            for (int i = 0; i < top; i++)
            {
                newTable.ImportRow((DataRow)rows[i]);
            }
            return newTable;
        }

        /// <summary>
        /// List转化一个DataTable  
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static DataTable ToDataTable<T>(this IEnumerable<T> list)
        {
            List<PropertyInfo> pList = new List<PropertyInfo>();
            Type type = typeof(T);
            DataTable dt = new DataTable();
            Array.ForEach<PropertyInfo>(type.GetProperties(), p => { pList.Add(p); dt.Columns.Add(p.Name); });
            foreach (var item in list)
            {
                DataRow row = dt.NewRow();
                pList.ForEach(p => row[p.Name] = p.GetValue(item, null));
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
