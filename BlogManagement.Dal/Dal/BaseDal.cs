using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Dal
{
    public class BaseDal
    {
        public IFreeSql Db => DbContext.Db;
    }
}
