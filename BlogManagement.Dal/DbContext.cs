using System;
using System.Collections.Generic;
using FreeSql;

namespace BlogManagement.Dal
{
    public class DbContext
    {

        public static Dictionary<string, IFreeSql> ConnectionPool = new Dictionary<string, IFreeSql>();
        private static string connectionString = ;


        public static IFreeSql Db
        {
            get
            {
                DataType t = DataType.SqlServer;
                return SelectDBType(t);
            }
        }

        private static IFreeSql SelectDBType(DataType enum_dbtype)
        {
            var dbtype = enum_dbtype.ToString();
            if (!ConnectionPool.ContainsKey(dbtype))
            {
                var freesql = new FreeSql.FreeSqlBuilder()
                    .UseConnectionString(enum_dbtype, connectionString)
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



                ConnectionPool.Add(dbtype, freesql);
            }
            return ConnectionPool[dbtype];
        }
    }
}
