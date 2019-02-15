using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo.soap
{
    /// <summary>
    /// 酒店房价api
    /// 提供了所有适用的规则、政策和限制, 以控制特定酒店房价的使用
    /// api 响应还可能包括所有费用的明细, 包括基本费率、住宿期间的变化、详细的税收以及总体估计成本
    /// </summary>
    public class HotelRateDescription : IActivity
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
        public HotelRateDescription(SoapServiceFactory soapServiceFactory, SessionPool sessionPool)
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
            HotelRateDescriptionLLSRQApi.Security1 security1 = new HotelRateDescriptionLLSRQApi.Security1() { BinarySecurityToken = security.BinarySecurityToken };
            var service = this.soapServiceFactory.CreateHotelRateDescriptionService(sharedContext.ConversationId, security1);
            var request = this.CreateRequest();
            try
            {
                var result = service.HotelRateDescriptionRQ(request);
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
        public HotelRateDescriptionLLSRQApi.HotelRateDescriptionRQ CreateRequest()
        {
            return new HotelRateDescriptionLLSRQApi.HotelRateDescriptionRQ()
            {
                AvailRequestSegment = new HotelRateDescriptionLLSRQApi.HotelRateDescriptionRQAvailRequestSegment()
                {
                    GuestCounts = new HotelRateDescriptionLLSRQApi.HotelRateDescriptionRQAvailRequestSegmentGuestCounts() { Count = "2" }
            ,
                    HotelSearchCriteria = new HotelRateDescriptionLLSRQApi.HotelRateDescriptionRQAvailRequestSegmentHotelSearchCriteria()
                    {
                        Criterion = new HotelRateDescriptionLLSRQApi.HotelRateDescriptionRQAvailRequestSegmentHotelSearchCriteriaCriterion()
                        { HotelRef = new HotelRateDescriptionLLSRQApi.HotelRateDescriptionRQAvailRequestSegmentHotelSearchCriteriaCriterionHotelRef() { HotelCode = "1" } }
                    }
            ,
                    RatePlanCandidates = new HotelRateDescriptionLLSRQApi.HotelRateDescriptionRQAvailRequestSegmentRatePlanCandidates()
                    { RatePlanCandidate = new HotelRateDescriptionLLSRQApi.HotelRateDescriptionRQAvailRequestSegmentRatePlanCandidatesRatePlanCandidate() { CurrencyCode = "USD", DCA_ProductCode = "A1B2C3D" } },
                    TimeSpan = new HotelRateDescriptionLLSRQApi.HotelRateDescriptionRQAvailRequestSegmentTimeSpan() { End = "1-31", Start = "2-25" }
                }
            };
        }
    }
}
