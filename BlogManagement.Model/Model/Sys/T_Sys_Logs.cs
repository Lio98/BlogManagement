using FreeSql.DataAnnotations;
using System;

namespace BlogManagement.Model
{
    /// <summary>
    ///  
    ///</summary>
    public class   T_Sys_Logs
    {

       public T_Sys_Logs()
       {
      
       }

        ///<summary>
        ///描述：标识
        ///</summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public long Id { get; set; }
        
        ///<summary>
        ///描述：操作  1、搜索  2、新增  3、修改  4、删除
        ///</summary>
        public string Operation { get; set; }
        
        ///<summary>
        ///描述：操作人
        ///</summary>
        public long Operator { get; set; }
        
        ///<summary>
        ///描述：操作类型 1、用户登录  2、系统异常  3、操作日志
        ///</summary>
        public string Type { get; set; }
        
        ///<summary>
        ///描述：内容
        ///</summary>
        public string Content { get; set; }

        /// <summary>
        /// 状态码
        /// </summary>
        public  int ResponseCode { get; set; }

        ///<summary>
        ///描述：创建人
        ///</summary>
        public DateTime CreateTime { get; set; } = DateTime.Now;

        ///<summary>
        ///描述：备注
        ///</summary>
        public string Remarks { get; set; }
        

    }
 }








