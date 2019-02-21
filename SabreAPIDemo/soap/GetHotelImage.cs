using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo.soap
{
    /// <summary>
    /// 获取酒店图像 api 
    /// </summary>
    public class GetHotelImage : IActivity
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
        public GetHotelImage(SoapServiceFactory soapServiceFactory, SessionPool sessionPool)
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
            GetHotelImageRQApi.Security security1 = new GetHotelImageRQApi.Security()
            { BinarySecurityToken = security.BinarySecurityToken, Actor = security.Actor };
            var service = this.soapServiceFactory.CreateGetHotelImageService(sharedContext.ConversationId, security1);
            var request = this.CreateRequest();
            try
            {
                Helper h = new Helper();
                string s = h.ToXml<GetHotelImageRQApi.MessageHeader>(service.MessageHeaderValue);
                string s2 = h.ToXml<GetHotelImageRQApi.Security>(service.SecurityValue);
                string s1 = h.ToXml<GetHotelImageRQApi.GetHotelImageRQ>(request);




                var result = service.GetHotelImageRQ(request);
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
        public GetHotelImageRQApi.GetHotelImageRQ CreateRequest()
        {
            return new GetHotelImageRQApi.GetHotelImageRQ()
            {
                HotelRefs = new GetHotelImageRQApi.HotelRefs() { HotelRef = new GetHotelImageRQApi.HotelRef[] { new GetHotelImageRQApi.HotelRef() { HotelCode = "0112773", CodeContext = "Sabre" } } },
                ImageRef = new GetHotelImageRQApi.ImageRef() { Type = GetHotelImageRQApi.ImageTypes.LARGE, CategoryCode = "1", LanguageCode = "CN" }
            };
        }
    }
}
