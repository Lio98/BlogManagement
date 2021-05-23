using FreeSql.DataAnnotations;
using System;

namespace BlogManagement.Model
{
    /// <summary>
    ///  权限
    ///</summary>
    public class T_Sys_Promise
    {

       public T_Sys_Promise()
       {
      
       }

        ///<summary>
        ///描述：标识
        ///</summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public int Id { get; set; }

        ///<summary>
        ///描述：角色标识
        ///</summary>
        public int RoleId { get; set; }

        ///<summary>
        ///描述：用户标识
        ///</summary>
        public int UserId { get; set; }

        ///<summary>
        ///描述：菜单标识
        ///</summary>
        public int MenuId { get; set; }

        ///<summary>
        ///描述：是否允许 - 0：否；1：是
        ///</summary>
        public string Allowed { get; set; }

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








