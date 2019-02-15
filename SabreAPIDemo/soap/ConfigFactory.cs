using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace SabreAPIDemo
{
    public static class ConfigFactory
    {
        /// <summary>
        /// SOAP配置提供程序
        /// </summary>
        private static readonly IConfigProvider SoapConfig = CreateForSoap();

        /// <summary>
        /// REST配置提供程序
        /// </summary>
        private static readonly IConfigProvider RestConfig = CreateForRest();

        /// <summary>
        /// 创建一个SOAP配置提供程序.
        /// </summary>
        /// <returns>Configuration provider.</returns>
        public static IConfigProvider CreateForSoap()
        {
            if (SoapConfig != null)
            {
                return SoapConfig;
            }
            return new ConfigProvider();
        }

        /// <summary>
        /// 创建一个REST配置提供程序.
        /// </summary>
        /// <returns>Configuration provider.</returns>
        public static IConfigProvider CreateForRest()
        {
            if (RestConfig != null)
            {
                return RestConfig;
            }
            return new ConfigProvider();
        }
    }
}
