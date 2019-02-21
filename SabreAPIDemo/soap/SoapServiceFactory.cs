using SessionCreateRQApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo
{
    public class SoapServiceFactory
    {
        /// <summary>
        /// 用作“来自参与方标识符”值的值
        /// </summary>
        private const string FromPartyId = "sample.url.of.sabre.client.com";

        /// <summary>
        /// 用作“来自参与方标识符”类型的值
        /// </summary>
        private const string FromType = "";

        /// <summary>
        /// 用作“对参与方标识符”值的值
        /// </summary>
        private const string ToPartyId = "webservices.sabre.com";

        /// <summary>
        /// 用作“对参与方标识符”类型的值
        /// </summary>
        private const string ToType = "";

        /// <summary>
        /// 配置提供对象
        /// </summary>
        private readonly IConfigProvider configProvider;

        /// <summary>
        /// 构造
        /// </summary>
        public SoapServiceFactory(IConfigProvider configProvider)
        {
            this.configProvider = configProvider;
        }

        /// <summary>
        /// 创建消息头.
        /// </summary>
        /// <param name="action">接口方法名.</param>
        /// <param name="conversationId">会话标识.</param>
        /// <returns>The message header</returns>
        public MessageHeader CreateHeader(string action, string conversationId)
        {
            string pseudoCityCode = this.configProvider.Group;

            return new MessageHeader
            {
                Service = new Service
                {
                    type = "OTA",
                    Value = action
                },
                Action = action,
                From = new From
                {
                    PartyId = new[]
                    {
                        new PartyId { type = FromType, Value = FromPartyId }
                    }
                },
                To = new To
                {
                    PartyId = new[]
                    {
                        new PartyId { type = ToType, Value = ToPartyId }
                    }
                },
                ConversationId = conversationId,
                CPAId = pseudoCityCode
            };
        }

        /// <summary>
        /// 创建“OTA_HotelAvailLLSRQApi”接口消息头
        /// </summary>
        /// <param name="action"></param>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public OTA_HotelAvailLLSRQApi.MessageHeader CreateHotelAvailLLSRQHeader(string action, string conversationId)
        {
            string pseudoCityCode = this.configProvider.Group;

            return new OTA_HotelAvailLLSRQApi.MessageHeader
            {
                Service = new OTA_HotelAvailLLSRQApi.Service
                {
                    type = "OTA",
                    Value = action
                },
                Action = action,
                From = new OTA_HotelAvailLLSRQApi.From
                {
                    PartyId = new[]
                    {
                        new OTA_HotelAvailLLSRQApi.PartyId { type = FromType, Value = FromPartyId }
                    }
                },
                To = new OTA_HotelAvailLLSRQApi.To
                {
                    PartyId = new[]
                    {
                        new OTA_HotelAvailLLSRQApi.PartyId { type = ToType, Value = ToPartyId }
                    }
                },
                ConversationId = conversationId,
                CPAId = pseudoCityCode
                ,
                MessageData = new OTA_HotelAvailLLSRQApi.MessageData() { Timeout = "10000" }
            };
        }

        /// <summary>
        /// 创建“HotelPropertyDescriptionLLSRQApi”接口消息头
        /// </summary>
        /// <param name="action"></param>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public HotelPropertyDescriptionLLSRQApi.MessageHeader CreateHotelPropertyDescriptionLLSRQHeader(string action, string conversationId)
        {
            string pseudoCityCode = this.configProvider.Group;
            return new HotelPropertyDescriptionLLSRQApi.MessageHeader()
            {
                Service = new HotelPropertyDescriptionLLSRQApi.Service
                {
                    type = "OTA",
                    Value = action
                },
                Action = action,
                From = new HotelPropertyDescriptionLLSRQApi.From
                {
                    PartyId = new[]
                    {
                        new HotelPropertyDescriptionLLSRQApi.PartyId { type = FromType, Value = FromPartyId }
                    }
                },
                To = new HotelPropertyDescriptionLLSRQApi.To
                {
                    PartyId = new[]
                    {
                        new HotelPropertyDescriptionLLSRQApi.PartyId { type = ToType, Value = ToPartyId }
                    }
                },
                ConversationId = conversationId,
                CPAId = pseudoCityCode
            };
        }

        /// <summary>
        /// 创建“HotelRateDescriptionLLSRQApi”接口消息头
        /// </summary>
        /// <param name="action"></param>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public HotelRateDescriptionLLSRQApi.MessageHeader CreateHotelRateDescriptionLLSRQHeader(string action, string conversationId)
        {
            string pseudoCityCode = this.configProvider.Group;
            return new HotelRateDescriptionLLSRQApi.MessageHeader()
            {
                Service = new HotelRateDescriptionLLSRQApi.Service
                {
                    type = "OTA",
                    Value = action
                },
                Action = action,
                From = new HotelRateDescriptionLLSRQApi.From
                {
                    PartyId = new[]
                    {
                        new HotelRateDescriptionLLSRQApi.PartyId { type = FromType, Value = FromPartyId }
                    }
                },
                To = new HotelRateDescriptionLLSRQApi.To
                {
                    PartyId = new[]
                    {
                        new HotelRateDescriptionLLSRQApi.PartyId { type = ToType, Value = ToPartyId }
                    }
                },
                ConversationId = conversationId,
                CPAId = pseudoCityCode
            };
        }

        /// <summary>
        /// 创建“PassengerDetailsRQApi”接口消息头
        /// </summary>
        /// <param name="action"></param>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public PassengerDetailsRQApi.MessageHeader CreatePassengerDetailsRQHeader(string action, string conversationId)
        {
            string pseudoCityCode = this.configProvider.Group;
            return new PassengerDetailsRQApi.MessageHeader()
            {
                Service = new PassengerDetailsRQApi.Service
                {
                    type = "OTA",
                    Value = action
                },
                Action = action,
                From = new PassengerDetailsRQApi.From
                {
                    PartyId = new[]
                    {
                        new PassengerDetailsRQApi.PartyId { type = FromType, Value = FromPartyId }
                    }
                },
                To = new PassengerDetailsRQApi.To
                {
                    PartyId = new[]
                    {
                        new PassengerDetailsRQApi.PartyId { type = ToType, Value = ToPartyId }
                    }
                },
                ConversationId = conversationId,
                CPAId = pseudoCityCode
            };
        }

        /// <summary>
        /// 创建“OTA_HotelResLLSRQApi”接口消息头
        /// </summary>
        /// <param name="action"></param>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public OTA_HotelResLLSRQApi.MessageHeader CreateHotelResLLSRQHeader(string action, string conversationId)
        {
            string pseudoCityCode = this.configProvider.Group;
            return new OTA_HotelResLLSRQApi.MessageHeader()
            {
                Service = new OTA_HotelResLLSRQApi.Service
                {
                    type = "OTA",
                    Value = action
                },
                Action = action,
                From = new OTA_HotelResLLSRQApi.From
                {
                    PartyId = new[]
                    {
                        new OTA_HotelResLLSRQApi.PartyId { type = FromType, Value = FromPartyId }
                    }
                },
                To = new OTA_HotelResLLSRQApi.To
                {
                    PartyId = new[]
                    {
                        new OTA_HotelResLLSRQApi.PartyId { type = ToType, Value = ToPartyId }
                    }
                },
                ConversationId = conversationId,
                CPAId = pseudoCityCode
            };
        }

        /// <summary>
        /// 创建“EndTransactionLLSRQApi”接口消息头
        /// </summary>
        /// <param name="action"></param>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public EndTransactionLLSRQApi.MessageHeader CreateEndTransactionLLSRQHeader(string action, string conversationId)
        {
            string pseudoCityCode = this.configProvider.Group;
            return new EndTransactionLLSRQApi.MessageHeader()
            {
                Service = new EndTransactionLLSRQApi.Service
                {
                    type = "OTA",
                    Value = action
                },
                Action = action,
                From = new EndTransactionLLSRQApi.From
                {
                    PartyId = new[]
                    {
                        new EndTransactionLLSRQApi.PartyId { type = FromType, Value = FromPartyId }
                    }
                },
                To = new EndTransactionLLSRQApi.To
                {
                    PartyId = new[]
                    {
                        new EndTransactionLLSRQApi.PartyId { type = ToType, Value = ToPartyId }
                    }
                },
                ConversationId = conversationId,
                CPAId = pseudoCityCode
            };
        }

        /// <summary>
        /// 创建“GetHotelImageRQApi”接口消息头
        /// </summary>
        /// <param name="action"></param>
        /// <param name="conversationId"></param>
        /// <returns></returns>
        public GetHotelImageRQApi.MessageHeader CreatGetHotelImageHeader(string action, string conversationId)
        {
            string pseudoCityCode = this.configProvider.Group;
            return new GetHotelImageRQApi.MessageHeader()
            {
                Service = new GetHotelImageRQApi.Service
                {
                    type = "OTA",
                    Value = action
                },
                Action = action,
                From = new GetHotelImageRQApi.From
                {
                    PartyId = new[]
                    {
                        new GetHotelImageRQApi.PartyId { type = FromType, Value = FromPartyId }
                    }
                },
                To = new GetHotelImageRQApi.To
                {
                    PartyId = new[]
                    {
                        new GetHotelImageRQApi.PartyId { type = ToType, Value = ToPartyId }
                    }
                },
                ConversationId = conversationId,
                CPAId = pseudoCityCode
            };
        }

        /// <summary>
        /// 创建“SessionCreate”请求对象.
        /// </summary>
        public SessionCreateRQApi.SessionCreateRQ CreateSessionCreateRequest()
        {
            return new SessionCreateRQ
            {
                POS = new SessionCreateRQPOS
                {
                    Source = new SessionCreateRQPOSSource
                    {
                        PseudoCityCode = this.configProvider.Group
                    }
                }
            };
        }

        /// <summary>
        /// 创建“SessionCreate”服务对象.
        /// </summary>
        public SessionCreateRQApi.SessionCreateRQService CreateSessionCreateService(string conversationId)
        {
            string endpoint = this.configProvider.Environment;

            MessageHeader header = this.CreateHeader("SessionCreateRQ", "AuthConversation");
            Security security = this.CreateUsernameSecurity();

            return new SessionCreateRQApi.SessionCreateRQService
            {
                MessageHeaderValue = header,
                SecurityValue = security,
                Url = endpoint
            };
        }

        /// <summary>
        /// 创建OTA_HotelAvailLLSRQApi服务对象
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="security"></param>
        /// <returns></returns>
        public OTA_HotelAvailLLSRQApi.OTA_HotelAvailService CreateOTA_HotelAvailService(string conversationId, OTA_HotelAvailLLSRQApi.Security1 security)
        {
            return new OTA_HotelAvailLLSRQApi.OTA_HotelAvailService
            {
                MessageHeaderValue = this.CreateHotelAvailLLSRQHeader("OTA_HotelAvailLLSRQ", conversationId),
                Security = security,
                Url = this.configProvider.Environment
            };
        }

        /// <summary>
        /// 创建HotelPropertyDescriptionLLSRQApi服务对象
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="security"></param>
        /// <returns></returns>
        public HotelPropertyDescriptionLLSRQApi.HotelPropertyDescriptionService CreateHotelPropertyDescriptionService(string conversationId, HotelPropertyDescriptionLLSRQApi.Security1 security)
        {
            return new HotelPropertyDescriptionLLSRQApi.HotelPropertyDescriptionService()
            {
                MessageHeaderValue = this.CreateHotelPropertyDescriptionLLSRQHeader("HotelPropertyDescriptionLLSRQ", conversationId),
                Security = security,
                Url = this.configProvider.Environment
            };
        }

        /// <summary>
        /// 创建HotelRateDescriptionLLSRQApi服务对象
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="security"></param>
        /// <returns></returns>
        public HotelRateDescriptionLLSRQApi.HotelRateDescriptionService CreateHotelRateDescriptionService(string conversationId, HotelRateDescriptionLLSRQApi.Security1 security)
        {
            return new HotelRateDescriptionLLSRQApi.HotelRateDescriptionService()
            {
                MessageHeaderValue = this.CreateHotelRateDescriptionLLSRQHeader("HotelRateDescriptionLLSRQ", conversationId),
                Security = security,
                Url = this.configProvider.Environment
            };
        }

        /// <summary>
        /// 创建PassengerDetailsRQApi服务对象
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="security"></param>
        /// <returns></returns>
        public PassengerDetailsRQApi.PassengerDetailsService CreatePassengerDetailsService(string conversationId, PassengerDetailsRQApi.Security security)
        {
            return new PassengerDetailsRQApi.PassengerDetailsService()
            {
                MessageHeaderValue = this.CreatePassengerDetailsRQHeader("PassengerDetailsRQ", conversationId),
                SecurityValue = security,
                Url = this.configProvider.Environment
            };
        }

        /// <summary>
        /// 创建OTA_HotelResLLSRQApi服务对象
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="security"></param>
        /// <returns></returns>
        public OTA_HotelResLLSRQApi.OTA_HotelResService CreateOTA_HotelResService(string conversationId, OTA_HotelResLLSRQApi.Security1 security)
        {
            return new OTA_HotelResLLSRQApi.OTA_HotelResService()
            {
                MessageHeaderValue = this.CreateHotelResLLSRQHeader("OTA_HotelResLLSRQ", conversationId),
                Security = security,
                Url = this.configProvider.Environment
            };
        }

        /// <summary>
        /// 创建EndTransactionLLSRQApi服务对象
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="security"></param>
        /// <returns></returns>
        public EndTransactionLLSRQApi.EndTransactionService CreateEndTransactionService(string conversationId, EndTransactionLLSRQApi.Security1 security)
        {
            return new EndTransactionLLSRQApi.EndTransactionService()
            {
                MessageHeaderValue = this.CreateEndTransactionLLSRQHeader("EndTransactionLLSRQ", conversationId),
                Security = security,
                Url = this.configProvider.Environment
            };
        }

        /// <summary>
        /// 创建GetHotelImageRQApi服务对象
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="security"></param>
        /// <returns></returns>
        public GetHotelImageRQApi.GetHotelImageRQService CreateGetHotelImageService(string conversationId, GetHotelImageRQApi.Security security)
        {
            return new GetHotelImageRQApi.GetHotelImageRQService()
            {
                MessageHeaderValue = this.CreatGetHotelImageHeader("GetHotelImageRQ", conversationId),
                SecurityValue = security,
                Url = this.configProvider.Environment
            };
        }

        /// <summary>
        /// 创建用于授权的对象.
        /// </summary>
        /// <returns>The security token.</returns>
        public Security CreateUsernameSecurity()
        {
            string userName = this.configProvider.UserId;
            string password = this.configProvider.ClientSecret;
            string domain = this.configProvider.Domain;
            string pseudoCityCode = this.configProvider.Group;

            return new Security
            {
                UsernameToken = new SecurityUsernameToken
                {
                    Username = userName,
                    Password = password,
                    Organization = pseudoCityCode,
                    Domain = domain
                }
            };
        }
    }
}
