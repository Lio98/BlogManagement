using FreeSql.DataAnnotations;
using System;

namespace BlogManagement.Model
{
    /// <summary>
    ///  用户角色映射表
    ///</summary>
    public class T_Sys_UserRole
    {

       public T_Sys_UserRole()
       {
      
       }

        ///<summary>
        ///描述：标识
        ///</summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        ///<summary>
        ///描述：用户标识
        ///</summary>
        public int UserId { get; set; }

        ///<summary>
        ///描述：角色标识
        ///</summary>
        public int RoleId { get; set; }

        ///<summary>
        ///描述：状态 - 0：禁用；1：正常
        ///</summary>
        public string Status { get; set; }

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








