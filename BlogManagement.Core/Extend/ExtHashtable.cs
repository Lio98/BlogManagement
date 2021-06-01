using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;

namespace BlogManagement.Core
{
    /// <summary>
    /// ExtHashtable
    /// </summary>
    public static class ExtHashtable
    {
        /// <summary>
        /// 实体转换Hashtable
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Hashtable ToHashtable<TEntity>(this TEntity entity)
        {
            Hashtable ht = new Hashtable();
            PropertyInfo[] ps = entity.GetType().GetProperties();
            foreach (PropertyInfo i in ps)
            {
                ht[i.Name] = i.GetValue(entity, null);
            }
            return ht;
        }
        /// <summary>
        /// DataRow  转  HashTable
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        public static Hashtable ToHashtable(this DataRow dr)
        {
            Hashtable htReturn = new Hashtable(dr.ItemArray.Length);
            foreach (DataColumn dc in dr.Table.Columns)
                htReturn.Add(dc.ColumnName, dr[dc.ColumnName]);
            return htReturn;
        }
    }
}
