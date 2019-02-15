using SessionCreateRQApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo
{
    /// <summary>
    /// SOAP身份验证服务
    /// </summary>
    public class SoapAuth
    {
        /// <summary>
        /// SOAP服务工厂
        /// </summary>
        private readonly SoapServiceFactory soapServiceFactory;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="soapServiceFactory">The SOAP service factory.</param>
        public SoapAuth(SoapServiceFactory soapServiceFactory)
        {
            this.soapServiceFactory = soapServiceFactory;
        }

        /// <summary>
        /// 创建一个新会话.
        /// </summary>
        /// <param name="conversationId">会话标识符.</param>
        public SoapResult<SessionCreateRS> CreateSession(string conversationId)
        {
            SessionCreateRQ request = this.soapServiceFactory.CreateSessionCreateRequest();
            SessionCreateRQService service = this.soapServiceFactory.CreateSessionCreateService(conversationId);

            var source = new TaskCompletionSource<SoapResult<SessionCreateRS>>();
            
            var result= service.SessionCreateRQ(request);

            return SoapResult<SessionCreateRS>.Success(result, service.SecurityValue);
        }

        /// <summary>
        /// 异步尝试执行刷新请求.
        /// </summary>
        /// <param name="security">登录信息.</param>
        /// <param name="conversationId">会话标识符.</param>
        /// <returns>The SOAP result of the Ping response.</returns>
        //public async Task<SoapResult<OTA_PingRS>> TryRefreshAsync(Security security, string conversationId)
        //{
        //    OTA_PingRQ request = this.soapServiceFactory.CreatePingRequest();
        //    SWSService service = this.soapServiceFactory.CreatePingService(conversationId, security);
        //    var source = new TaskCompletionSource<SoapResult<OTA_PingRS>>();
        //    service.OTA_PingRQCompleted += (s, e) =>
        //    {
        //        if (SoapHelper.HandleErrors(e, source))
        //        {
        //            if (e.Result.Items.Any())
        //            {
        //                ErrorsType error = e.Result.Items[0] as ErrorsType;
        //                if (error != null && error.Error != null && error.Error.Length > 0)
        //                {
        //                    source.TrySetResult(SoapResult<OTA_PingRS>.Error(error.Error.First()));
        //                    return;
        //                }
        //            }

        //            source.TrySetResult(SoapResult<OTA_PingRS>.Success(e.Result));
        //        }
        //    };
        //    service.OTA_PingRQAsync(request);
        //    return await source.Task;
        //}
    }
}
