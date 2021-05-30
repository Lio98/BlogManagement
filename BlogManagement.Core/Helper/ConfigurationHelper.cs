using System;
using System.Collections.Generic;
using System.Text;
using BlogManagement.Model;
using Microsoft.Extensions.Configuration;

namespace BlogManagement.Core
{
    public class ConfigurationHelper
    {
        public static IConfiguration Configuration = Appsettings.Configuration;

        public static string GetValue(string key)
        {
            return Configuration.GetValue<string>(key);
        }

        /// <summary>
        /// 获取数据库链接字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetDbConnection(string key)
        {
            DbModel dbModel = Get<DbModel>(key);
            string connString = dbModel.DevelopDatabase;
            switch (dbModel.DB)
            {
                case "ProductDatabase":
                    connString = dbModel.ProductDatabase;
                    break;
                case "DevelopDatabase":
                    connString = dbModel.DevelopDatabase;
                    break;
                default:
                    break;
            }
            if (dbModel.IsEncrypt)
            {
                //connString = Victory.Core.Encrypt.Aes.DecryptString(connString, Core.Helpers.MachineHelper.GetCpuId());
            }

            return connString;
        }

        public static string GetSingle(string key)
        {
            return Configuration.GetSection(key).Value;
        }

        public static T Get<T>(string key)
        {
            return Configuration.GetSection(key).Get<T>();
        }
    }
}
