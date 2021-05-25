using BlogManagement.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using BlogManagement.Core;
using BlogManagement.Core.Redis.Service;
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
        public T_Sys_User GetUserInfoById(long id)
        {
            T_Sys_User userInfo;
            using (RedisHashService service=new RedisHashService())
            {
                userInfo = service.GetFromHash<T_Sys_User>(id);
                if (userInfo?.Id==id)
                    return userInfo;
            }
            userInfo= Db.Select<T_Sys_User>().Where(i => i.Id == id).ToList().FirstOrDefault();
            //查出来再存到缓存中
            if (userInfo?.Id == id)
            {
                using (RedisHashService service=new RedisHashService())
                {
                    service.StoreAsHash(userInfo);
                }
            }

            return userInfo;
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
            long id = Db.Insert<T_Sys_User>(user).ExecuteIdentity();
            if (id <= 0) 
                return false;

            //写入缓存
            using (RedisHashService service=new RedisHashService())
            {
                user.Id = id;
                service.StoreAsHash(user);
            }

            return true;
        }
    }
}
