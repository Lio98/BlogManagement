using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Core
{
    /// <summary>
    /// Excel到处模板
    /// </summary>
    public class ExcelTemplateModel
    {
        /// <summary>
        /// 行号
        /// </summary>
        public int row { get; set; }
        /// <summary>
        /// 列号
        /// </summary>
        public int cell { get; set; }
        /// <summary>
        /// 数据值
        /// </summary>
        public string value { get; set; }
    }
}
