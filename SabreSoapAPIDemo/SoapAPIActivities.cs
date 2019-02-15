using OTA_HotelAvailServiceDemo;
using SabreAPIDemo;
using SabreSoapAPIDemo.soap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreSoapAPIDemo
{
    public class SoapAPIActivities
    {
        /// <summary>
        /// The SOAP service factory
        /// </summary>
        private readonly SoapServiceFactory soapServiceFactory;

        /// <summary>
        /// The session pool
        /// </summary>
        private readonly ISessionPool sessionPool;

        public SoapAPIActivities(SoapServiceFactory soapServiceFactory, ISessionPool sessionPool)
        {
            this.sessionPool = sessionPool;
            this.soapServiceFactory = soapServiceFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IActivity RunActivity(SharedContext sharedContext)
        {
            var security = this.sessionPool.TakeSessionAsync(sharedContext.ConversationId);
            var service = this.soapServiceFactory.CreateOTA_HotelAvailService(sharedContext.ConversationId, security);


            var data = new OTA_HotelAvailRQ()
            {
                AvailRequestSegment = new OTA_HotelAvailRQAvailRequestSegment()
                {
                    Customer = new OTA_HotelAvailRQAvailRequestSegmentCustomer()
                    {
                        Corporate = new OTA_HotelAvailRQAvailRequestSegmentCustomerCorporate() { ID = "abc" }
                    }
            ,
                    GuestCounts = new OTA_HotelAvailRQAvailRequestSegmentGuestCounts() { Count = "2" },
                    HotelSearchCriteria = new OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteria() { Criterion = new OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteriaCriterion() { HotelRef = new OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteriaCriterionHotelRef[1] { new OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteriaCriterionHotelRef() { HotelCityCode = "DFW" } } } }
            ,
                    TimeSpan = new OTA_HotelAvailRQAvailRequestSegmentTimeSpan() { End = "1-31", Start = "2-15" }
                }
            };

            service.OTA_HotelAvailRQ(data);

            return null;
        }
    }
}
