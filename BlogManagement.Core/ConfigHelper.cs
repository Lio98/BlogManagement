using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace BlogManagement.Core
{
    public class ConfigHelper
    {
        private static IConfiguration _configuration;

        public ConfigHelper(string filename) => this.BuildConfiguration(filename);

        private void BuildConfiguration(string filename) => _configuration = (IConfiguration)new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile(filename).Build();

        public string GetSingle(string key) => _configuration.GetSection(key).Value;

        public T Get<T>(string key) => _configuration.GetSection(key).Get<T>();
    }
}
