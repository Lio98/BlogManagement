using BlogManagement.Model;
using System;
using System.Collections.Generic;

namespace BlogManagement.Interface
{
    public interface IUser
    {
        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        List<T_Sys_User> GetAllUserInfo();

        /// <summary>
        /// 根据用户ID查询用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T_Sys_User GetUserInfoById(long id);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        bool UserLogin(string account, string password);

        /// <summary>
        /// 通过Id更新用户信息
        /// </summary>
        /// <returns></returns>
        bool UpdateUserInfoById(int id);

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool AddUserInfo(T_Sys_User user);
    }
}
