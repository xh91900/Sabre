using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo.soap
{
    /// <summary>
    /// 添加额外的(必需的)信息来创建乘客记录(PNR)
    /// 引用旅客个人资料中的唯一身份证，或者您可以提供旅行者信息以及代理和票务信息
    /// 出售OTH，MCO，PTA或INS类型的杂项
    /// 添加基本的、字母编码的、客户地址、送货地址、发票、行程、组名、历史记录、隐藏或公司编号备注
    /// 可以包括支付细节和未来队列放置
    /// 结束事务，将数据保存在PNR和Sabre主机系统中
    /// 可以选择向客户发送电子邮件，通知客户发票在virtual There网站上可用
    /// 将PNR放在编号队列上
    /// 您可以使用此服务创建 pnr, 方法是为最多99名旅客添加旅客信息, 也可以向现有 pnr 和旅客添加备注和 ssr
    /// </summary>
    public class PassengerDetails : IActivity
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
        public PassengerDetails(SoapServiceFactory soapServiceFactory, SessionPool sessionPool)
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
            PassengerDetailsRQApi.Security security1 = new PassengerDetailsRQApi.Security()
            { BinarySecurityToken = security.BinarySecurityToken, Actor = security.Actor };
            var service = this.soapServiceFactory.CreatePassengerDetailsService(sharedContext.ConversationId, security1);
            var request = this.CreateRequest();
            try
            {
                var result = service.PassengerDetailsRQ(request);
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
        public PassengerDetailsRQApi.PassengerDetailsRQ CreateRequest()
        {
            return new PassengerDetailsRQApi.PassengerDetailsRQ()
            {
                PostProcessing = new PassengerDetailsRQApi.PassengerDetailsRQPostProcessing()
                { IgnoreAfter = false, RedisplayReservation = true, UnmaskCreditCard = true }
            ,
                PreProcessing = new PassengerDetailsRQApi.PassengerDetailsRQPreProcessing()
                { IgnoreBefore = true, UniqueID = new PassengerDetailsRQApi.PassengerDetailsRQPreProcessingUniqueID() { ID = "" } }
            ,
                SpecialReqDetails = new PassengerDetailsRQApi.PassengerDetailsRQSpecialReqDetails()
                {
                    SpecialServiceRQ = new PassengerDetailsRQApi.PassengerDetailsRQSpecialReqDetailsSpecialServiceRQ()
                    {
                        SpecialServiceInfo = new PassengerDetailsRQApi.PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfo()
                        {
                            AdvancePassenger = new PassengerDetailsRQApi.PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfoAdvancePassenger[]
            { new PassengerDetailsRQApi.PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfoAdvancePassenger()
            { SegmentNumber = "A" ,Document=new PassengerDetailsRQApi.PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfoAdvancePassengerDocument()
            { ExpirationDate=DateTime.Parse("2019-05-26"),Number="18382099231",Type="P",IssueCountry="FR",NationalityCountry="FR"}
            ,PersonName=new PassengerDetailsRQApi.PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfoAdvancePassengerPersonName()
            { DateOfBirth=DateTime.Parse("1980-12-02"),Gender=PassengerDetailsRQApi.PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfoAdvancePassengerPersonNameGender.M,NameNumber="1.1",DocumentHolder=true,GivenName="JAMES",MiddleName="RS",Surname="GREEN"}
            ,VendorPrefs=new PassengerDetailsRQApi.PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfoAdvancePassengerVendorPrefs()
            { Airline=new PassengerDetailsRQApi.PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfoAdvancePassengerVendorPrefsAirline()
            { Hosted=false} } } }
                        }
                    }
                }
            ,
                TravelItineraryAddInfoRQ = new PassengerDetailsRQApi.PassengerDetailsRQTravelItineraryAddInfoRQ()
                {
                    AgencyInfo = new PassengerDetailsRQApi.PassengerDetailsRQTravelItineraryAddInfoRQAgencyInfo()
                    {
                        Address = new PassengerDetailsRQApi.PassengerDetailsRQTravelItineraryAddInfoRQAgencyInfoAddress()
                        {
                            AddressLine = "SABRE TRAVEL",
                            CityName = "SOUTHLAKE",
                            CountryCode = "US",
                            PostalCode = "76092"
            ,
                            StateCountyProv = new PassengerDetailsRQApi.PassengerDetailsRQTravelItineraryAddInfoRQAgencyInfoAddressStateCountyProv()
                            { StateCode = "TX" },
                            StreetNmbr = "3150 SABRE DRIVE",
                            VendorPrefs = new PassengerDetailsRQApi.PassengerDetailsRQTravelItineraryAddInfoRQAgencyInfoAddressVendorPrefs()
                            { Airline = new PassengerDetailsRQApi.PassengerDetailsRQTravelItineraryAddInfoRQAgencyInfoAddressVendorPrefsAirline() { Hosted = false } }
                        }
                    }
            ,
                    CustomerInfo = new PassengerDetailsRQApi.PassengerDetailsRQTravelItineraryAddInfoRQCustomerInfo()
                    {
                        ContactNumbers = new PassengerDetailsRQApi.PassengerDetailsRQTravelItineraryAddInfoRQCustomerInfoContactNumber[1]
                        { new PassengerDetailsRQApi.PassengerDetailsRQTravelItineraryAddInfoRQCustomerInfoContactNumber()
                        { NameNumber = "1.1", Phone = "682-605-1000", PhoneUseType = "H" } }
            ,
                        PersonName = new PassengerDetailsRQApi.PassengerDetailsRQTravelItineraryAddInfoRQCustomerInfoPersonName[1]
                        { new PassengerDetailsRQApi.PassengerDetailsRQTravelItineraryAddInfoRQCustomerInfoPersonName()
                        { NameNumber = "1.1", NameReference = "NameReference", PassengerType = "ADT", GivenName = "Ram", Surname = "Jee" } }
                    }
                }
            };
        }
    }
}
