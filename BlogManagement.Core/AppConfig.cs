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
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsetting.json").Build();

            ConfigHelper configHelper = new ConfigHelper("appsettings.json");
            DbModel dbModel = configHelper.Get<DbModel>("ConnectionStrings");

            string connString = dbModel.DevelopDatabase;
            if (dbModel.DB == "ProductDatabase")
            {
                connString = dbModel.ProductDatabase;
            }

            if (dbModel.DB == "EnvironDatabase")
            {
                connString = dbModel.EnvironDatabase;
            }

            if (dbModel.DB == "DevelopDatabase")
            {
                connString = dbModel.DevelopDatabase;
            }


            if (dbModel.IsEncrypt)
            {
                //connString = Victory.Core.Encrypt.Aes.DecryptString(connString, Core.Helpers.MachineHelper.GetCpuId());
            }

            return connString;
        }
    }
}
