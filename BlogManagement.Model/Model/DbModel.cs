using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Model
{
    public class DbModel
    {
        /// <summary>
        /// 使用数据库
        /// </summary>
        public string DB { get; set; }

        /// <summary>
        /// 使用数据库
        /// </summary>
        public bool IsEncrypt { get; set; }

        /// <summary>
        /// 投产环境
        /// </summary>
        public string ProductDatabase { get; set; }

        /// <summary>
        /// 预投产环境
        /// </summary>
        public string EnvironDatabase { get; set; }

        /// <summary>
        /// 开发环境
        /// </summary>
        public string DevelopDatabase { get; set; }
    }
}
