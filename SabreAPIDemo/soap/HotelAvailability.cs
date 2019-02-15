using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo
{
    /// <summary>
    /// 酒店供应情况
    /// 使用基本的机场代码、城市代码或城市名称搜索
    /// 最多可提前 331天, 最多可入住9位客人, 最多可入住220天
    /// </summary>
    public class HotelAvailability : IActivity
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
        public HotelAvailability(SoapServiceFactory soapServiceFactory, SessionPool sessionPool)
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
            OTA_HotelAvailLLSRQApi.Security1 security1 = new OTA_HotelAvailLLSRQApi.Security1() { BinarySecurityToken = security.BinarySecurityToken };
            var service = this.soapServiceFactory.CreateOTA_HotelAvailService(sharedContext.ConversationId, security1);
            var request = this.CreateRequest();

            try
            {
                var result = service.OTA_HotelAvailRQ(request);
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
        public OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQ CreateRequest()
        {
            return new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQ()
            {
                AvailRequestSegment = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegment()
                {
                    Customer = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentCustomer()
                    {
                        Corporate = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentCustomerCorporate() { ID = "ABC123" }
                    },
                    GuestCounts = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentGuestCounts() { Count = "1" },
                    HotelSearchCriteria = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteria()
                    {
                        Criterion = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteriaCriterion()
                        {
                            HotelRef = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteriaCriterionHotelRef[1]
                        { new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteriaCriterionHotelRef() { HotelCityCode = "bjs" } }
                        }
                    },
                    TimeSpan = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentTimeSpan()
                    {
                        Start = "02-15",
                        End = "02-18"
                    }
                },
                Version = "2.3.0"
            };
        }
    }
}
