using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SabreAPIDemo
{
    /// <summary>
    /// 接口配置信息
    /// </summary>
    public class ConfigProvider : IConfigProvider
    {

        /// <summary>
        /// 构造
        /// </summary>
        public ConfigProvider()
        {

        }

        /// <summary>
        /// group
        /// </summary>
        public string Group
        {
            get { return this.GetProperty("group"); }
        }

        /// <summary>
        /// userId
        /// </summary>
        public string UserId
        {
            get { return this.GetProperty("userId"); }
        }

        /// <summary>
        /// clientSecret
        /// </summary>
        public string ClientSecret
        {
            get { return this.GetProperty("clientSecret"); }
        }

        /// <summary>
        /// domain
        /// </summary>
        public string Domain
        {
            get { return this.GetProperty("domain"); }
        }

        /// <summary>
        /// 接口地址
        /// </summary>
        public string Environment
        {
            get { return this.GetProperty("environment"); }
        }

        /// <summary>
        /// 从配置文件中获取配置
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        private string GetProperty(string property)
        {
            try
            {
                return System.Configuration.ConfigurationManager.AppSettings[property].ToString();
            }
            catch (Exception)
            {
                throw new KeyNotFoundException("配置信息错误");
            }
        }
    }
}
