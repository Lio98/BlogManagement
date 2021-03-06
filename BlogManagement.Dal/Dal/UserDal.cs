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
        /// 用户登录
        /// </summary>
        /// <param name="account"></param>
        /// <param name="password"></param>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public bool UserLogin(string account, string password, out T_Sys_User userInfo)
        {
            string encryptPassword = EncryptHelper.MD5Encrypt(password);
            userInfo = Db.Select<T_Sys_User>().Where(i =>
                    i.Account == account
                    && i.Password.Equals(encryptPassword, StringComparison.CurrentCultureIgnoreCase)
                    && i.Status.Equals("1"))
                .ToList()
                .FirstOrDefault();
            return userInfo != null ? true : false;
        }

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
            using (RedisHashService service = new RedisHashService())
            {
                userInfo = service.GetFromHash<T_Sys_User>(id);
                if (userInfo?.Id == id)
                    return userInfo;
            }
            userInfo = Db.Select<T_Sys_User>().Where(i => i.Id == id).ToList().FirstOrDefault();
            //查出来再存到缓存中
            if (userInfo?.Id == id)
            {
                using (RedisHashService service = new RedisHashService())
                {
                    service.StoreAsHash(userInfo);
                }
            }

            return userInfo;
        }

        /// <summary>
        /// 新增用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool AddUserInfo(T_Sys_User user, out string msg)
        {
            msg = string.Empty;
            //检验该账户是否已存在
            var existUser = Db.Select<T_Sys_User>()
                .Where(i => i.Account.Equals(user.Account, StringComparison.CurrentCultureIgnoreCase)).First();
            if (existUser != null)
            {
                msg = "该账号已存在！";
                return false;
            }

            //密码MD5加密
            user.Password = EncryptHelper.MD5Encrypt(user.Password);
            user.CreateTime = DateTime.Now;
            user.UpdateTime = DateTime.Now;
            long id = Db.Insert<T_Sys_User>(user).ExecuteIdentity();

            if (id <= 0)
            {
                msg = "新增用户失败，请联系系统管理员！";
                return false;
            }

            //写入缓存
            using (RedisHashService service = new RedisHashService())
            {
                user.Id = id;
                service.StoreAsHash(user);
            }

            return true;
        }

        /// <summary>
        /// 通过Id更新用户信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool UpdateUserInfoById(long id)
        {

            return false;
        }
    }
}
