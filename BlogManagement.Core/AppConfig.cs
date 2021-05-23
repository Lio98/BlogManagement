using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using BlogManagement.Model;
using Microsoft.Extensions.Configuration;

namespace BlogManagement.Core
{
    public class AppConfig
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        /// <returns></returns>
        public static string ConnectionString()
        {
            ConfigHelper configHelper = new ConfigHelper("appsettings.json");
            DbModel dbModel = configHelper.Get<DbModel>("ConnectionStrings");

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
    }
}
