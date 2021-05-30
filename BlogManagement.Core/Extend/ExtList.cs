using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogManagement.Core
{
    public static class ExtList
    {
        /// <summary>
        /// List分页
        /// </summary>
        /// <param name="data">表数据</param>
        /// <param name="pagination">分页参数</param>
        /// <returns></returns>
        public static List<T> GetPage<T>(List<T> data, PageInput pagination)
        {
            return data.Skip(pagination.CurrentPage * pagination.PageSize - pagination.PageSize).Take(pagination.PageSize).ToList();
        }
        /// <summary>
        /// IList转成List<T>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> IListToList<T>(IList list)
        {
            T[] array = new T[list.Count];
            list.CopyTo(array, 0);
            return new List<T>(array);
        }
    }
}
