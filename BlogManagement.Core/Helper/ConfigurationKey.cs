using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlogManagement.Core
{
    public static class ConfigurationKey
    {

        private static string SystemPath = Appsettings.app(new string[] { "JNPF_App", "SystemPath" });
        /// <summary>
        /// 数据库驱动：EF、Dapper
        /// </summary>
        public static string DbConnectionDrive
        {
            get
            {
                return ConfigurationHelper.GetValue("DbConnectionDrive");
            }
        }
        /// <summary>
        /// 多租户启用：true、false
        /// </summary>
        public static bool MultiTenancy
        {
            get
            {
                var flag = ConfigurationHelper.GetValue("MultiTenancy");
                if (flag.IsNotEmptyOrNull())
                {
                    return flag.ToBool();
                }
                return false;
            }
        }
        /// <summary>
        /// Redis缓存地址
        /// </summary>
        public static string RedisConnectionString
        {
            get
            {
                return Appsettings.app(new string[] { "JNPF_App", "Redis", "ConnectionString" });
            }
        }
        /// <summary>
        /// SignalR服务地址
        /// </summary>
        public static string SignalRUrl
        {
            get
            {
                return ConfigurationHelper.GetValue("SignalRUrl");
            }
        }
        /// <summary>
        /// 数据库备份文件路径
        /// </summary>
        public static string DataBackupFilePath
        {
            get
            {
                var filePath = "~/DataBackupFile/";

                if (filePath.IndexOf("~/") == -1)
                {
                    return filePath;
                }
                else if (!SystemPath.IsNullOrEmpty())
                {
                    return SystemPath + filePath.Replace("~/", "");
                }
                else
                {
                    return Directory.GetCurrentDirectory() + filePath.Replace("~/", "");
                }

            }
        }
        /// <summary>
        /// 临时文件存储路径
        /// </summary>
        public static string TemporaryFilePath
        {
            get
            {
                var filePath = "~/TemporaryFile/";
                if (filePath.IndexOf("~/") == -1)
                {
                    return filePath;
                }
                else if (!SystemPath.IsNullOrEmpty())
                {
                    return SystemPath + filePath.Replace("~/", "");
                }
                else
                {
                    return Directory.GetCurrentDirectory() + filePath.Replace("~/", "");
                }

            }
        }
        /// <summary>
        /// 系统文件存储路径
        /// </summary>
        public static string SystemFilePath
        {
            get
            {
                var filePath = "~/SystemFile/";
                if (filePath.IndexOf("~/") == -1)
                {
                    return filePath;
                }
                else if (!SystemPath.IsNullOrEmpty())
                {
                    return SystemPath + filePath.Replace("~/", "");
                }
                else
                {
                    return Directory.GetCurrentDirectory() + filePath.Replace("~/", "");
                }
            }
        }
        /// <summary>
        /// 文件模板存储路径
        /// </summary>
        public static string TemplateFilePath
        {
            get
            {
                var filePath = "~/TemplateFile/";
                if (filePath.IndexOf("~/") == -1)
                {
                    return filePath;
                }
                else if (!SystemPath.IsNullOrEmpty())
                {
                    return SystemPath + filePath.Replace("~/", "");
                }
                else
                {
                    return Directory.GetCurrentDirectory() + filePath.Replace("~/", "");
                }
            }
        }
        /// <summary>
        /// 邮件文件存储路径
        /// </summary>
        public static string EmailFilePath
        {
            get
            {
                var filePath = "~/EmailFile/";
                if (filePath.IndexOf("~/") == -1)
                {
                    return filePath;
                }
                else if (!SystemPath.IsNullOrEmpty())
                {
                    return SystemPath + filePath.Replace("~/", "");
                }
                else
                {
                    return Directory.GetCurrentDirectory() + filePath.Replace("~/", "");
                }
            }
        }
        /// <summary>
        /// 文档管理存储路径
        /// </summary>
        public static string DocumentFilePath
        {
            get
            {
                var filePath = "~/DocumentFile/";
                if (filePath.IndexOf("~/") == -1)
                {
                    return filePath;
                }
                else if (!SystemPath.IsNullOrEmpty())
                {
                    return SystemPath + filePath.Replace("~/", "");
                }
                else
                {
                    return Directory.GetCurrentDirectory() + filePath.Replace("~/", "");
                }
            }
        }
        /// <summary>
        /// 文件在线预览存储PDF
        /// </summary>
        public static string DocumentPreviewFilePath
        {
            get
            {
                var filePath = "~/DocumentPreview/";
                if (filePath.IndexOf("~/") == -1)
                {
                    return filePath;
                }
                else if (!SystemPath.IsNullOrEmpty())
                {
                    return SystemPath + filePath.Replace("~/", "");
                }
                else
                {
                    return Directory.GetCurrentDirectory() + filePath.Replace("~/", "");
                }
            }
        }
        /// <summary>
        /// 用户头像存储路径
        /// </summary>
        public static string UserAvatarFilePath
        {
            get
            {
                var filePath = "~/UserAvatar/";
                if (filePath.IndexOf("~/") == -1)
                {
                    return filePath;
                }
                else if (!SystemPath.IsNullOrEmpty())
                {
                    return SystemPath + filePath.Replace("~/", "");
                }
                else
                {
                    return Directory.GetCurrentDirectory() + filePath.Replace("~/", "");
                }
            }
        }
        /// <summary>
        /// IM聊天图片+语音存储路径
        /// </summary>
        public static string IMContentFilePath
        {
            get
            {
                var filePath = "~/IMContentFile/";
                if (filePath.IndexOf("~/") == -1)
                {
                    return filePath;
                }
                else if (!SystemPath.IsNullOrEmpty())
                {
                    return SystemPath + filePath.Replace("~/", "");
                }
                else
                {
                    return Directory.GetCurrentDirectory() + filePath.Replace("~/", "");
                }
            }
        }
        /// <summary>
        /// 微信公众号资源存储路径
        /// </summary>
        public static string MPMaterialFilePath
        {
            get
            {
                var filePath = "~/MPMaterial/";
                if (filePath.IndexOf("~/") == -1)
                {
                    return filePath;
                }
                else if (!SystemPath.IsNullOrEmpty())
                {
                    return SystemPath + filePath.Replace("~/", "");
                }
                else
                {
                    return Directory.GetCurrentDirectory() + filePath.Replace("~/", "");
                }
            }
        }
        /// <summary>
        /// 微信公众号允许上传文件类型
        /// </summary>
        public static string[] MPUploadFileType
        {
            get
            {
                return ConfigurationHelper.GetValue("MPUploadFileType").Split(',');
            }
        }
        /// <summary>
        /// 微信允许上传文件类型
        /// </summary>
        public static string[] WeChatUploadFileType
        {
            get
            {
                return ConfigurationHelper.GetValue("WeChatUploadFileType").Split(',');
            }
        }
        /// <summary>
        /// 允许图片类型
        /// </summary>
        public static string[] AllowUploadImageType
        {
            get
            {
                return ConfigurationHelper.GetValue("AllowUploadImageType").Split(',');
            }
        }
        /// <summary>
        /// 允许上传文件类型
        /// </summary>
        public static string[] AllowUploadFileType
        {
            get
            {
                return ConfigurationHelper.GetValue("AllowUploadFileType").Split(',');
            }
        }
        /// <summary>
        /// 前端文件目录
        /// </summary>
        public static string ServiceDirectory
        {
            get
            {
                return ConfigurationHelper.GetValue("ServiceDirectory");
            }
        }
        /// <summary>
        /// 后端文件目录
        /// </summary>
        public static string WebDirectory
        {
            get
            {
                return ConfigurationHelper.GetValue("WebDirectory");
            }
        }
        /// <summary>
        /// 软件名称
        /// </summary>
        public static string SoftName
        {
            get
            {
                return ConfigurationHelper.GetValue("SoftName");
            }
        }
        /// <summary>
        /// 软件全称
        /// </summary>
        public static string SoftFullName
        {
            get
            {
                return ConfigurationHelper.GetValue("SoftFullName");
            }
        }
        /// <summary>
        /// 软件版本
        /// </summary>
        public static string SoftVersion
        {
            get
            {
                return ConfigurationHelper.GetValue("SoftVersion");
            }
        }
        /// <summary>
        /// APP版本
        /// </summary>
        public static string AppVersion
        {
            get
            {
                return ConfigurationHelper.GetValue("AppVersion");
            }
        }
        /// <summary>
        /// APP版本更新内容
        /// </summary>
        public static string AppUpdataContent
        {
            get
            {
                return ConfigurationHelper.GetValue("AppUpdataContent");
            }
        }
        /// <summary>
        /// 软件的错误报告
        /// </summary>
        public static bool ErrorReport
        {
            get
            {
                return ConfigurationHelper.GetValue("ErrorReport").ToBool();
            }
        }
        /// <summary>
        /// 软件的错误报告发给谁
        /// </summary>
        public static string ErrorReportTo
        {
            get
            {
                return ConfigurationHelper.GetValue("ErrorReportTo");
            }
        }
        /// <summary>
        /// 阿里云短信accessKeyId
        /// </summary>
        public static string aliyuncs_accessKeyId
        {
            get
            {
                return ConfigurationHelper.GetValue("aliyuncs_accessKeyId");
            }
        }
        /// <summary>
        /// 阿里云短信accessKeySecret
        /// </summary>
        public static string aliyuncs_accessKeySecret
        {
            get
            {
                return ConfigurationHelper.GetValue("aliyuncs_accessKeySecret");
            }
        }
        /// <summary>
        /// 推送是否启动：false、true
        /// </summary>
        public static bool igexin_enabled
        {
            get
            {
                return ConfigurationHelper.GetValue("igexin_enabled").ToBool();
            }
        }
        /// <summary>
        /// APPID
        /// </summary>
        public static string igexin_appid
        {
            get
            {
                return ConfigurationHelper.GetValue("igexin_appid");
            }
        }
        /// <summary>
        /// APPKEY
        /// </summary>
        public static string igexin_appkey
        {
            get
            {
                return ConfigurationHelper.GetValue("igexin_appkey");
            }
        }
        /// <summary>
        /// MASTERSECRET
        /// </summary>
        public static string igexin_mastersecret
        {
            get
            {
                return ConfigurationHelper.GetValue("igexin_mastersecret");
            }
        }

        public static string BiVisualFilePath
        {
            get
            {
                var filePath = "~/BiVisualPath/";
                if (filePath.IndexOf("~/") == -1)
                {
                    return filePath;
                }
                else if (!SystemPath.IsNullOrEmpty())
                {
                    return SystemPath + filePath.Replace("~/", "");
                }
                else
                {
                    return Directory.GetCurrentDirectory() + filePath.Replace("~/", "");
                }
            }
        }

        /// <summary>
        /// 代码生成器文件路径
        /// </summary>
        public static string CodeGeneratorFilePath
        {
            get
            {
                var filePath = "~/CodeGenerate/";
                if (filePath.IndexOf("~/") == -1)
                {
                    return filePath;
                }
                else if (!SystemPath.IsNullOrEmpty())
                {
                    return SystemPath + filePath.Replace("~/", "");
                }
                else
                {
                    return Directory.GetCurrentDirectory() + filePath.Replace("~/", "");
                }
            }
        }
    }
}
