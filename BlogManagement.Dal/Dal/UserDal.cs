using BlogManagement.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BlogManagement.Core;
using BlogManagement.Interface;

namespace BlogManagement.Dal
{
    public class UserDal : BaseDal, IUser
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
        /// 根据Id查找用户信息/登录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T_Sys_User GetUserInfoById(int id)
        {
            return Db.Select<T_Sys_User>().Where(i => i.Id == id).ToList().FirstOrDefault();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool UserLogin(string account, string password)
        {
            string encryptPassword = Encrypt.MD5Encrypt(password);
            var userInfo = Db.Select<T_Sys_User>().Where(i =>
                    i.Account == account
                    && i.Password.Equals(encryptPassword, StringComparison.CurrentCultureIgnoreCase)
                    && i.Status.Equals("1"))
                .ToList()
                .FirstOrDefault();
            return userInfo != null ? true : false;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <returns></returns>
        public bool UpdateUserInfo()
        {

            return false;
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool AddUserInfo(T_Sys_User user)
        {
            //密码MD5加密
            user.Password = Encrypt.MD5Encrypt(user.Password);
            user.CreateTime=DateTime.Now;
            user.UpdateTime=DateTime.Now;
            return Db.Insert<T_Sys_User>(user).ExecuteAffrows() > 0 ? true : false;
        }
    }
}
