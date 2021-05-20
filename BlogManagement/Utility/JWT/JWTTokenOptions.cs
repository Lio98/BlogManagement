using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement.Utility.JWT
{
    public class JWTTokenOptions
    {
        /// <summary>
        /// 订阅者
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// 发起人
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// 过期时间  单位 秒
        /// </summary>
        public long Expire { get; set; }

        /// <summary>
        /// 密钥
        /// </summary>
        public string Secret { get; set; }
    }
}
