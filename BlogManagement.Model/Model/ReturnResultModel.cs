using System;
using System.Collections.Generic;
using System.Text;
using BlogManagement.Model.Enum;

namespace BlogManagement.Model.Model
{
    public class ReturnResultModel
    {
        public int StatusCode { get; set; }

        public ReturnStatus Status { get; set; }

        public string Data { get; set; }

        public string Msg { get; set; }
    }
}
