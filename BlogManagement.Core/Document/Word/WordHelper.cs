using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;

namespace BlogManagement.Core
{
    public class WordHelper
    {
        /// <summary>
        /// 把DataTable导出为Word文件
        /// </summary>
        /// <param name="fileName">Word文件名(不包括后缀*.doc)</param>
        /// <param name="data">将要被导出的对象</param>
        /// <param name="titles">标题</param>
        /// <returns></returns>
        public static void Export(string fileName, DataTable data, Dictionary<string, string> titles, string filePath)
        {
            //var httpContext = HttpContext.Current;
            //httpContext.Response.ContentType = "application/ms-word";
            //httpContext.Response.Headers.Add("Content-Disposition", "attachment;filename=" + fileName + ".doc");
            //httpContext.Response.Body.Write(DataTableToHtmlTable(data, titles));
            //httpContext.Response.Body.Flush();
            //httpContext.Response.Body.Close();
            var byteList = DataTableToHtmlTable(data, titles);
            ExportWrite(filePath, byteList);
        }
        /// <summary>
        /// 把List导出为Word文件
        /// </summary>
        /// <param name="fileName">Word文件名(不包括后缀*.doc)</param>
        /// <param name="data">将要被导出的对象</param>
        /// <param name="titles">标题</param>
        /// <returns></returns>
        public static void Export<T>(string fileName, List<T> data, Dictionary<string, string> titles, string filePath)
        {
            try
            {
                var byteList = ListToHtmlTable(data, titles);
                ExportWrite(filePath, byteList);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }



        }
        /// <summary>
        /// 把List转换成Html的Table
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data">对象数据</param>
        /// <param name="titles">标题</param>
        /// <returns></returns>
        private static byte[] ListToHtmlTable<T>(List<T> data, Dictionary<string, string> titles)
        {
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            //写出列名
            sbHtml.Append("<tr style=\"background-color: #FFE88C;font-weight: bold; white-space: nowrap;\">");
            foreach (var item in titles)
            {
                sbHtml.Append(string.Format("<td>{0}</td>", item.Value));
            }
            sbHtml.Append("</tr>");
            //写数据
            foreach (T entity in data)
            {
                Hashtable ht = entity.ToHashtable<T>();
                sbHtml.Append("<tr>");
                foreach (var item in titles)
                {
                    sbHtml.Append(string.Format("<td>{0}</td>", ht[item.Key]));
                }
                sbHtml.Append("</tr>");
            }
            sbHtml.Append("</table>");
            var byteList = Encoding.Default.GetBytes(sbHtml.ToString());
            return byteList;
        }
        /// <summary>
        /// 把DataTable转换成Html的Table
        /// </summary>
        /// <param name="data">对象数据</param>
        /// <param name="titles">标题</param>
        /// <returns></returns>
        private static byte[] DataTableToHtmlTable(DataTable data, Dictionary<string, string> titles)
        {
            StringBuilder sbHtml = new StringBuilder();
            sbHtml.Append("<table cellspacing=\"0\" rules=\"all\" border=\"1\" style=\"border-collapse:collapse;\">");
            //写出列名
            sbHtml.Append("<tr style=\"background-color: #FFE88C;font-weight: bold; white-space: nowrap;\">");
            foreach (var item in titles)
            {
                sbHtml.Append(string.Format("<td>{0}</td>", item.Value));
            }
            sbHtml.Append("</tr>");
            //写数据
            foreach (DataRow dr in data.Rows)
            {
                sbHtml.Append("<tr>");
                foreach (var item in titles)
                {
                    sbHtml.Append(string.Format("<td>{0}</td>", dr[item.Key].ToString()));
                }
                sbHtml.Append("</tr>");
            }
            sbHtml.Append("</table>");
            return Encoding.Default.GetBytes(sbHtml.ToString());
        }


        public static void ExportWrite(string filePath, byte[] buffer)
        {
            try
            {
                using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write))
                {
                    fs.Write(buffer, 0, buffer.Length);
                    fs.Flush();
                    buffer = null;
                }//使用using可以最后不用关闭fs 比较方便
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
