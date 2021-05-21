using System;
using System.IO;
using BlogManagement.Model;
using Microsoft.Extensions.Configuration;

namespace BlogManager.Core
{
    public class AppConfig
    {
        public static string ConnectionString()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsetting.json").Build();

            DbModel dbModel = configuration.GetSection("ConnectionStrings").Get<DbModel>();

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
