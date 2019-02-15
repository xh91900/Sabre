using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo.soap
{
    /// <summary>
    /// 酒店属性描述
    /// 按房间和房价类型分列的单个物业的可用房价的详细信息
    /// 根据向酒店供应商提出的实时请求以及在要求时提供的实际价格和房间
    /// api 允许用户提供速率代码和限定符来购买适用的费率
    /// </summary>
    public class HotelPropertyDescription : IActivity
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
        public HotelPropertyDescription(SoapServiceFactory soapServiceFactory, SessionPool sessionPool)
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
            HotelPropertyDescriptionLLSRQApi.Security1 security2 = new HotelPropertyDescriptionLLSRQApi.Security1()
            { BinarySecurityToken = security.BinarySecurityToken, Actor = security.Actor, DidUnderstand = true };
            HotelPropertyDescriptionLLSRQApi.HotelPropertyDescriptionService hotelPropertyDescriptionService = this.soapServiceFactory.CreateHotelPropertyDescriptionService(sharedContext.ConversationId, security2);
            var request = this.CreateRequest();

            try
            {
                HotelPropertyDescriptionLLSRQApi.HotelPropertyDescriptionRS hotelPropertyDescriptionRS = hotelPropertyDescriptionService.HotelPropertyDescriptionRQ(request);
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
        public HotelPropertyDescriptionLLSRQApi.HotelPropertyDescriptionRQ CreateRequest()
        {
            return new HotelPropertyDescriptionLLSRQApi.HotelPropertyDescriptionRQ()
            {
                AvailRequestSegment = new HotelPropertyDescriptionLLSRQApi.HotelPropertyDescriptionRQAvailRequestSegment()
                {
                    GuestCounts = new HotelPropertyDescriptionLLSRQApi.HotelPropertyDescriptionRQAvailRequestSegmentGuestCounts() { Count = "2" },
                    HotelSearchCriteria = new HotelPropertyDescriptionLLSRQApi.HotelPropertyDescriptionRQAvailRequestSegmentHotelSearchCriteria()
                    {
                        Criterion = new HotelPropertyDescriptionLLSRQApi.HotelPropertyDescriptionRQAvailRequestSegmentHotelSearchCriteriaCriterion()
                        {
                            HotelRef = new HotelPropertyDescriptionLLSRQApi.HotelPropertyDescriptionRQAvailRequestSegmentHotelSearchCriteriaCriterionHotelRef()
                            { HotelCode = "1" }
                        }
                    },
                    TimeSpan = new HotelPropertyDescriptionLLSRQApi.HotelPropertyDescriptionRQAvailRequestSegmentTimeSpan()
                    {
                        Start = "02-13",
                        End = "02-15"
                    },
                    POS = new HotelPropertyDescriptionLLSRQApi.HotelPropertyDescriptionRQAvailRequestSegmentPOS()
                    {
                        Source = new HotelPropertyDescriptionLLSRQApi.HotelPropertyDescriptionRQAvailRequestSegmentPOSSource()
                        { CompanyName = new HotelPropertyDescriptionLLSRQApi.HotelPropertyDescriptionRQAvailRequestSegmentPOSSourceCompanyName() { Division = "" } }
                    }
                }
            };
        }
    }
}
