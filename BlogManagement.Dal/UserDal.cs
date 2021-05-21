using BlogManagement.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Dal
{
    public class UserDal
    {
        public T_Sys_User GetUserInfoById(int id) 
        {
            //var info = DbContext.Db.Ado.Query();
            return new T_Sys_User()
            {
                Id = 123
            };
        }
    }
}
