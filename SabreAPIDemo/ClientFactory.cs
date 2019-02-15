using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class ClientFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static RestClient CreateRestClient()
        {
            IConfigProvider config = ConfigFactory.CreateForRest();
            RestAuthorizationManager restAuthorizationManager = new RestAuthorizationManager(config);
            return new RestClient(restAuthorizationManager, config);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        //public static SoapClient CreateSoapClient()
        //{
        //    IConfigProvider config = ConfigFactory.CreateForSoap();
        //    RestAuthorizationManager restAuthorizationManager = new RestAuthorizationManager(config);
        //    return new SoapClient(restAuthorizationManager, config);
        //}
    }
}
