using System;
using System.Collections.Generic;
using BlogManagement.Core;
using FreeSql;

namespace BlogManagement.Dal
{
    public static class DbContext
    {

        public static Dictionary<string, IFreeSql> ConnectionPool = new Dictionary<string, IFreeSql>();


        public static IFreeSql Db
        {
            get
            {
                DataType t = DataType.SqlServer;
                return SelectDbType(t);
            }
        }

        private static IFreeSql SelectDbType(DataType enumDbtype)
        {
            var dbType = enumDbtype.ToString();
            if (!ConnectionPool.ContainsKey(dbType))
            {
                var freeSql = new FreeSql.FreeSqlBuilder()
                    .UseConnectionString(enumDbtype, ConfigurationHelper.GetDbConnection("ConnectionStrings"))
                    .UseAutoSyncStructure(false)   //是否根据实体修改数据库， Code-First
                    .UseMonitorCommand(
                        cmd =>
                        {

                            Console.WriteLine("============================================");
                            Console.WriteLine("");
                            Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                            Console.WriteLine("");
                            Console.WriteLine(cmd.CommandText);

                            Console.WriteLine("============================================");
                        }
                    )
                    .UseLazyLoading(true)
                    .Build();



                ConnectionPool.Add(dbType, freeSql);
            }
            return ConnectionPool[dbType];
        }
    }
}
