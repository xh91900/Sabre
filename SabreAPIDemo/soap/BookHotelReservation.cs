using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo.soap
{
    /// <summary>
    /// 用于预订一个或多个酒店客房, 并在乘客姓名记录 (pnr) 中创建酒店细分市场
    /// 此 api 允许客户端应用程序从酒店描述中销售酒店预订
    /// 包含至少名称和地址的 pnr 必须位于当前会话/sabre 工作区域中
    /// 要从酒店描述中预订房间, 客户端应用程序必须从 "酒店物业描述 api 响应消息" 中获取相关的行号
    /// </summary>
    public class BookHotelReservation : IActivity
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
        public BookHotelReservation(SoapServiceFactory soapServiceFactory, SessionPool sessionPool)
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
            OTA_HotelResLLSRQApi.Security1 security1 = new OTA_HotelResLLSRQApi.Security1()
            { BinarySecurityToken = security.BinarySecurityToken, Actor = security.Actor };
            var service = this.soapServiceFactory.CreateOTA_HotelResService(sharedContext.ConversationId, security1);
            var request = this.CreateRequest();
            try
            {
                var result = service.OTA_HotelResRQ(request);
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
        public OTA_HotelResLLSRQApi.OTA_HotelResRQ CreateRequest()
        {
            return new OTA_HotelResLLSRQApi.OTA_HotelResRQ()
            {
                Hotel = new OTA_HotelResLLSRQApi.OTA_HotelResRQHotel()
                {
                    BasicPropertyInfo = new OTA_HotelResLLSRQApi.OTA_HotelResRQHotelBasicPropertyInfo() { RPH = "2" },
                    Guarantee = new OTA_HotelResLLSRQApi.OTA_HotelResRQHotelGuarantee()
                    {
                        Type = "GDPST",
                        CC_Info = new OTA_HotelResLLSRQApi.OTA_HotelResRQHotelGuaranteeCC_Info()
                        {
                            PaymentCard = new OTA_HotelResLLSRQApi.OTA_HotelResRQHotelGuaranteeCC_InfoPaymentCard() { Code = "AX", ExpireDate = "2019-12", Number = "1234567890" }
            ,
                            PersonName = new OTA_HotelResLLSRQApi.OTA_HotelResRQHotelGuaranteeCC_InfoPersonName() { Surname = "TEST" }
                        }
                    },
                    RoomType = new OTA_HotelResLLSRQApi.OTA_HotelResRQHotelRoomType() { NumberOfUnits = "1" },
                    SpecialPrefs = new OTA_HotelResLLSRQApi.OTA_HotelResRQHotelSpecialPrefs() { WrittenConfirmation = new OTA_HotelResLLSRQApi.OTA_HotelResRQHotelSpecialPrefsWrittenConfirmation() { Ind = true } }
                }
            };
        }
    }
}
