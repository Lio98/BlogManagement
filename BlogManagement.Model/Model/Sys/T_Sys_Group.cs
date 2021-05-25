using FreeSql.DataAnnotations;
using System;

namespace BlogManagement.Model
{
    /// <summary>
    ///  组
    ///</summary>
    public class T_Sys_Group
    {

       public T_Sys_Group()
       {
      
       }

        ///<summary>
        ///描述：标识
        ///</summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public long Id { get; set; }

        ///<summary>
        ///描述：编码
        ///</summary>
        public string Code { get; set; }

        ///<summary>
        ///描述：名称
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///描述：状态 - 0：禁用；1：正常
        ///</summary>
        public string Status { get; set; }

        ///<summary>
        ///描述：备注
        ///</summary>
        public string Remark { get; set; }

        ///<summary>
        ///描述：创建人
        ///</summary>
        public long Creater { get; set; }

        ///<summary>
        ///描述：创建时间
        ///</summary>
        public DateTime CreateTime { get; set; }

        ///<summary>
        ///描述：修改人
        ///</summary>
        public long Updater { get; set; }

        ///<summary>
        ///描述：修改时间
        ///</summary>
        public DateTime UpdateTime { get; set; }

    }
 }








