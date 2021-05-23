using FreeSql.DataAnnotations;
using System;

namespace BlogManagement.Model
{
    /// <summary>
    ///  角色表
    ///</summary>
    public class T_Sys_Role
    {

       public T_Sys_Role()
       {
      
       }

        ///<summary>
        ///描述：标识
        ///</summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        ///<summary>
        ///描述：名称
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///描述：编码
        ///</summary>
        public string Code { get; set; }

        ///<summary>
        ///描述：状态 - 0：禁用；1：正常
        ///</summary>
        public string Status { get; set; }

        ///<summary>
        ///描述：备注
        ///</summary>
        public string Reamrk { get; set; }

        ///<summary>
        ///描述：创建人
        ///</summary>
        public int Creater { get; set; }

        ///<summary>
        ///描述：创建时间
        ///</summary>
        public DateTime CreateTime { get; set; }

        ///<summary>
        ///描述：修改人
        ///</summary>
        public int Updater { get; set; }

        ///<summary>
        ///描述：修改时间
        ///</summary>
        public DateTime UpdateTime { get; set; }

    }
 }








