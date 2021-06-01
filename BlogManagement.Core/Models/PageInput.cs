using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Core
{
    /// <summary>
    /// 分页信息输入
    /// </summary>
    public class PageInput
    {
        /// <summary>
        /// 当前页码:pageIndex
        /// </summary>
        public int CurrentPage { get; set; } = 1;
        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { set; get; } = 50;
        /// <summary>
        /// 排序字段:sortField
        /// </summary>
        public string Sidx { get; set; }
        /// <summary>
        /// 排序类型:sortType
        /// </summary>
        public string Sord { get; set; } = "desc";
        /// <summary>
        /// 查询关键字
        /// </summary>
        public string Keyword { get; set; }
        /// <summary>
        /// 查询条件
        /// </summary>
        public string QueryJson { get; set; } = null;
    }
}
