using FreeSql.DataAnnotations;
using System;

namespace BlogManagement.Model
{
    /// <summary>
    ///  菜单表
    ///</summary>
    public class T_Sys_Menu
    {

       public T_Sys_Menu()
       {
      
       }

        ///<summary>
        ///描述：标识
        ///</summary>
        [Column(IsIdentity = true, IsPrimary = true)]
        public long Id { get; set; }

        ///<summary>
        ///描述：方法名称
        ///</summary>
        public string ActionName { get; set; }

        ///<summary>
        ///描述：模块名称
        ///</summary>
        public string ModuleName { get; set; }

        ///<summary>
        ///描述：仅文件夹 - 0：否；1：是
        ///</summary>
        public string FolderOnly { get; set; }

        ///<summary>
        ///描述：是否继承 - 0：否；1：是
        ///</summary>
        public string IsInherit { get; set; }

        ///<summary>
        ///描述：类名
        ///</summary>
        public string ClassName { get; set; }

        ///<summary>
        ///描述：方法名
        ///</summary>
        public string MethodName { get; set; }

        ///<summary>
        ///描述：显示菜单上 - 0：否；1：是
        ///</summary>
        public string ShowOnMenu { get; set; }

        ///<summary>
        ///描述：是否公开 - 0：否；1：是
        ///</summary>
        public string IsPublic { get; set; }

        ///<summary>
        ///描述：显示命令
        ///</summary>
        public long DispalyOrder { get; set; }

        ///<summary>
        ///描述：是否内部 - 0：否；1：是
        ///</summary>
        public string IsInside { get; set; }

        ///<summary>
        ///描述：Url
        ///</summary>
        public string Url { get; set; }

        ///<summary>
        ///描述：图标
        ///</summary>
        public string Icon { get; set; }

        ///<summary>
        ///描述：父项标识
        ///</summary>
        public long ParentId { get; set; }

        ///<summary>
        ///描述：
        ///</summary>
        public string Status { get; set; }

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








