using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo.soap
{
    /// <summary>
    /// 结束乘客姓名记录的事务
    /// 用于完成和存储对乘客姓名记录（PNR）所做的更改
    /// 更新现有记录时请注意，必须在调用EndTransactionLLSRQ之前执行TravelItineraryReadRQ
    /// </summary>
    public class EndTransaction : IActivity
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
        public EndTransaction(SoapServiceFactory soapServiceFactory, SessionPool sessionPool)
        {
            this.sessionPool = sessionPool;
            this.soapServiceFactory = soapServiceFactory;
        }

        /// <summary>
        /// 运行活动
        /// 执行成功返回下一个活动
        /// </summary>
        /// <param name="sharedContext"></param>
        /// <returns></returns>
        public IActivity Run(SharedContext sharedContext)
        {
            var security = this.sessionPool.TakeSessionAsync(sharedContext.ConversationId);
            EndTransactionLLSRQApi.Security1 security1 = new EndTransactionLLSRQApi.Security1()
            { BinarySecurityToken = security.BinarySecurityToken, Actor = security.Actor };
            var service = this.soapServiceFactory.CreateEndTransactionService(sharedContext.ConversationId, security1);
            var request = this.CreateRequest();
            try
            {
                var result = service.EndTransactionRQ(request);
                return null;
            }
            catch (Exception ex)
            {
                // 日志
                sharedContext.IsFaulty = true;
                this.sessionPool.ReleaseSession(sharedContext.ConversationId);
                return null;
            }
        }

        /// <summary>
        /// 获取请求参数对象
        /// </summary>
        /// <returns></returns>
        public EndTransactionLLSRQApi.EndTransactionRQ CreateRequest()
        {
            return new EndTransactionLLSRQApi.EndTransactionRQ()
            { EndTransaction = new EndTransactionLLSRQApi.EndTransactionRQEndTransaction() { Ind = true }, Source = new EndTransactionLLSRQApi.EndTransactionRQSource() { ReceivedFrom = "SWS TEST" } };
        }
    }
}
