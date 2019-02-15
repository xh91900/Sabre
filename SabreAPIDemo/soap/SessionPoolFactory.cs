using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo
{
    /// <summary>
    /// 会话池静态工厂
    /// </summary>
    public static class SessionPoolFactory
    {
        /// <summary>
        /// 会话池大小
        /// </summary>
        private const int DefaultSessionPoolSize = 1;

        /// <summary>
        /// 创建会话
        /// </summary>
        /// <returns></returns>
        public static SessionPool Create()
        {
            IConfigProvider config = ConfigFactory.CreateForSoap();
            var sessionPool = new SessionPool(new SoapAuth(new SoapServiceFactory(config)), DefaultSessionPoolSize);
            //Task.Run(() => sessionPool.Populate());
            sessionPool.Populate();
            return sessionPool;
        }
    }
}
