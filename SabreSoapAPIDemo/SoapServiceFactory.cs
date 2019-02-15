using OTA_HotelAvailServiceDemo;
using SabreAPIDemo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreSoapAPIDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class SoapServiceFactory
    {
        /// <summary>
        /// The configuration provider
        /// </summary>
        private readonly IConfigProvider configProvider;

        /// <summary>
        /// Value used as "from party identifier" value
        /// </summary>
        private const string FromPartyId = "sample.url.of.sabre.client.com";

        /// <summary>
        /// Value used as "from party identifier" type
        /// </summary>
        private const string FromType = "";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="conversationId"></param>
        /// <param name="security"></param>
        /// <returns></returns>
        public OTA_HotelAvailService CreateOTA_HotelAvailService(string conversationId, Security1 security)
        {
            return new OTA_HotelAvailService
            {
                MessageHeaderValue = this.CreateHeader("OTA_HotelAvailRQ", conversationId),
                Security = security,
            };
        }

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
    }
}
