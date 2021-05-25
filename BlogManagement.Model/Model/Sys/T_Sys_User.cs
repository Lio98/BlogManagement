using FreeSql.DataAnnotations;
using System;


namespace BlogManagement.Model
{
    /// <summary>
    ///  用户表
    ///</summary>
    public class   T_Sys_User
    {

       public T_Sys_User()
       {
      
       }

        ///<summary>
        ///描述：标识
        ///</summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public long Id { get; set; }

        ///<summary>
        ///描述：名称
        ///</summary>
        public string Name { get; set; }

        ///<summary>
        ///描述：账号
        ///</summary>
        public string Account { get; set; }

        ///<summary>
        ///描述：密码
        ///</summary>
        public string Password { get; set; }

        ///<summary>
        ///描述：状态 - 0：禁用；1正常
        ///</summary>
        public string Status { get; set; }

        ///<summary>
        ///描述：备注
        ///</summary>
        public string Remark { get; set; }

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








