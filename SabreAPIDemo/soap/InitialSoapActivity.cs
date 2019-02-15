using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo
{
    /// <summary>
    /// SOAP工作流的初始活动。设置下一个活动要使用的数据.
    /// </summary>
    public class InitialSoapActivity : IActivity
    {
        /// <summary>
        /// SOAP服务工厂
        /// </summary>
        private readonly SoapServiceFactory soapServiceFactory;

        /// <summary>
        /// 会话池
        /// </summary>
        private readonly SessionPool sessionPool;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="soapServiceFactory"></param>
        /// <param name="sessionPool"></param>
        public InitialSoapActivity(SoapServiceFactory soapServiceFactory, SessionPool sessionPool)
        {
            this.soapServiceFactory = soapServiceFactory;
            this.sessionPool = sessionPool;
        }

        /// <summary>
        /// 按步骤执行活动
        /// </summary>
        /// <param name="sharedContext"></param>
        /// <returns></returns>
        public IActivity Run(SharedContext sharedContext)
        {
            sharedContext.AddResult("", "请求参数对象");
            IActivity nextActivity = new HotelAvailability(this.soapServiceFactory, this.sessionPool);
            return nextActivity;
        }
    }
}
