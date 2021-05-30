using System;
using System.Collections.Generic;
using System.Text;

namespace BlogManagement.Core
{
    public class FileManage
    {
        /// <summary>
        /// 添加附件：将临时文件夹的文件拷贝到正式文件夹里面
        /// </summary>
        /// <param name="data"></param>
        public static void CreateFile(List<FileModel> data)
        {
            if (data != null && data.Count > 0)
            {
                var temporaryFilePath = ConfigurationKey.TemporaryFilePath;
                var systemFilePath = ConfigurationKey.SystemFilePath;
                foreach (FileModel item in data)
                {
                    FileHelper.MoveFile(temporaryFilePath + item.FileId, systemFilePath + item.FileId);
                }
            }
        }
        /// <summary>
        /// 更新附件
        /// </summary>
        /// <param name="data"></param>
        public static void UpdateFile(List<FileModel> data)
        {
            if (data != null)
            {
                var temporaryFilePath = ConfigurationKey.TemporaryFilePath;
                var systemFilePath = ConfigurationKey.SystemFilePath;
                foreach (FileModel item in data)
                {
                    if (item.FileType == "add")
                    {
                        FileHelper.MoveFile(temporaryFilePath + item.FileId, systemFilePath + item.FileId);
                    }
                    else if (item.FileType == "delete")
                    {
                        FileHelper.DeleteFile(systemFilePath + item.FileId);
                    }
                }
            }
        }
        /// <summary>
        /// 删除附件
        /// </summary>
        /// <param name="data"></param>
        public static void DeleteFile(List<FileModel> data)
        {
            if (data != null && data.Count > 0)
            {
                var systemFilePath = ConfigurationKey.SystemFilePath;
                foreach (FileModel item in data)
                {
                    FileHelper.DeleteFile(systemFilePath + item.FileId);
                }
            }
        }
    }
}
