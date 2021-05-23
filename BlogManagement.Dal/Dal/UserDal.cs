using BlogManagement.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BlogManagement.Dal
{
    public class UserDal:BaseDal
    {
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        public List<T_Sys_User> GetAllUserInfo()
        {
            return Db.Select<T_Sys_User>().ToList();
        }

        /// <summary>
        /// 根据Id查找用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_Sys_User GetUserInfoById(int id)
        {
            return Db.Select<T_Sys_User>().Where(i => i.Id == id).ToList().FirstOrDefault();
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <returns></returns>
        public bool UpdateUserInfo()
        {
            
            return false;
        }
    }
}
