using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Core
{
    public class FileModel
    {
        public string FileId { get; set; }
        public string FileName { get; set; }
        public string FileSize { get; set; }
        public DateTime FileTime { get; set; }
        public string FileState { get; set; }
        public string FileType { get; set; }
    }
}
