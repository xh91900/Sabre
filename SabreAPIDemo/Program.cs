using SessionCreateRQApi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using System.Text;

namespace SabreAPIDemo
{
    class Program
    {
        private const string FromType = "";
        private static IConfigProvider configProvider;
        private const string FromPartyId = "sample.url.of.sabre.client.com";
        private const string ToType = "";
        private const string ToPartyId = "webservices.sabre.com";
        private const string Alphabet = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private static readonly Random Rand = new Random();

        static void Main(string[] args)
        {
            // Sets the TLS 1.2 as default security protocol
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            // soap接口调用流程
            SessionPool SessionPool = SessionPoolFactory.Create();
            IActivity activity = new InitialSoapActivity(new SoapServiceFactory(ConfigFactory.CreateForSoap()), SessionPool);
            Workflow workflow = new Workflow(activity);
            SharedContext sharedContext = workflow.Run();







            Helper hp = new Helper();
            configProvider = ConfigFactory.CreateForSoap();
            string endpoint = configProvider.Environment;
            MessageHeader header = CreateHeader("SessionCreateRQ", "AuthConversation");
            Security security = CreateUsernameSecurity();
            SessionCreateRQService sessionCreateRQService = new SessionCreateRQService
            { MessageHeaderValue = header, SecurityValue = security, Url = endpoint };
            SessionCreateRS sessionCreateRS = sessionCreateRQService.SessionCreateRQ(CreateSessionCreateRequest());


            SessionCreateRQ request = CreateSessionCreateRequest();
            SessionCreateRQService service = new SessionCreateRQService()
            { MessageHeaderValue = header, SecurityValue = security, Url = endpoint };

            //SessionCreateRS response= service.SessionCreateRQ(request);
            //SoapResult<SessionCreateRS> soapRs= SoapResult<SessionCreateRS>.Success(response, service.SecurityValue);

            var source = new TaskCompletionSource<SoapResult<SessionCreateRS>>();
            service.SessionCreateRQCompleted += (a, e) =>
            {
                if (SoapHelper.HandleErrors(e, source))
                {
                    source.TrySetResult(SoapResult<SessionCreateRS>.Success(e.Result, service.SecurityValue));
                }
                Console.WriteLine(service.SecurityValue.BinarySecurityToken);

                string longRandom = new string(Enumerable.Range(0, 8).Select(i => Alphabet[Rand.Next(Alphabet.Length)]).ToArray());
                string conversationId = DateTime.Now.ToString("YYYYMMddhhmmss") + "-" + longRandom;

                OTA_HotelAvailLLSRQApi.Security1 security1 = new OTA_HotelAvailLLSRQApi.Security1()
                { BinarySecurityToken = service.SecurityValue.BinarySecurityToken, Actor = security.Actor, DidUnderstand = true };
                OTA_HotelAvailLLSRQApi.OTA_HotelAvailService hotelAvailService = new SoapServiceFactory
                (ConfigFactory.CreateForSoap()).CreateOTA_HotelAvailService(conversationId, security1);

                var rq = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQ()
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


                OTA_HotelAvailLLSRQApi.OTA_HotelAvailRS hotelAvailRS = hotelAvailService.OTA_HotelAvailRQ(rq);

                string RSXml = hp.ToXml<OTA_HotelAvailLLSRQApi.OTA_HotelAvailRS>(hotelAvailRS);

                

            };


            service.SessionCreateRQAsync(request);



            // Return the asynchronous task.
            //return await source.Task;


            //OTA_HotelAvailRS rs = new OTA_HotelAvailRS();
            //string s = JsonConvert.SerializeObject(rs);

            //HotelPropertyDescriptionRS rs1 = new HotelPropertyDescriptionRS();
            //string s1 = JsonConvert.SerializeObject(rs1);

            //HotelRateDescriptionRS rs2 = new HotelRateDescriptionRS();
            //string s2 = JsonConvert.SerializeObject(rs2);

            //PassengerDetailsRS rs3 = new PassengerDetailsRS();
            //string s3 = JsonConvert.SerializeObject(rs3);

            //OTA_HotelResRS rs4 = new OTA_HotelResRS();
            //string s4 = JsonConvert.SerializeObject(rs4);

            //EndTransactionRS rs5 = new EndTransactionRS();
            //string s5 = JsonConvert.SerializeObject(rs5);


            //// 步骤1:检索酒店可用性
            //OTA_HotelAvailService hotelAvailService = new OTA_HotelAvailService();
            //var rq = new OTA_HotelAvailRQ()
            //{
            //    AvailRequestSegment = new OTA_HotelAvailRQAvailRequestSegment()
            //    {
            //        Customer = new OTA_HotelAvailRQAvailRequestSegmentCustomer()
            //        {
            //            Corporate = new OTA_HotelAvailRQAvailRequestSegmentCustomerCorporate() { ID = "abc" }
            //        }
            //,
            //        GuestCounts = new OTA_HotelAvailRQAvailRequestSegmentGuestCounts() { Count = "2" },
            //        HotelSearchCriteria = new OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteria() { Criterion = new OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteriaCriterion() { HotelRef = new OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteriaCriterionHotelRef[1] { new OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteriaCriterionHotelRef() { HotelCityCode = "DFW" } } } }
            //,
            //        TimeSpan = new OTA_HotelAvailRQAvailRequestSegmentTimeSpan() { End = "1-31", Start = "2-15" }
            //    }
            //};
            //string s = JsonConvert.SerializeObject(rq);
            //OTA_HotelAvailRS hotelAvailRS = hotelAvailService.OTA_HotelAvailRQ(rq);


            //// 步骤2:检索酒店价格
            //HotelPropertyDescriptionService hotelPropertyDescriptionService = new HotelPropertyDescriptionService();
            //var rq = new HotelPropertyDescriptionRQ()
            //{
            //    AvailRequestSegment = new HotelPropertyDescriptionRQAvailRequestSegment()
            //    {
            //        GuestCounts = new HotelPropertyDescriptionRQAvailRequestSegmentGuestCounts() { Count = "2" }
            //,
            //        HotelSearchCriteria = new HotelPropertyDescriptionRQAvailRequestSegmentHotelSearchCriteria() { Criterion = new HotelPropertyDescriptionRQAvailRequestSegmentHotelSearchCriteriaCriterion() { HotelRef = new HotelPropertyDescriptionRQAvailRequestSegmentHotelSearchCriteriaCriterionHotelRef() { HotelCode = "1" } } }
            //,
            //        TimeSpan = new HotelPropertyDescriptionRQAvailRequestSegmentTimeSpan() { End = "1-31", Start = "2-25" }
            //    }
            //};
            //string s = JsonConvert.SerializeObject(rq);
            //HotelPropertyDescriptionRS hotelPropertyDescriptionRS = hotelPropertyDescriptionService.HotelPropertyDescriptionRQ(rq);

            // 步骤3:检索酒店规则和策略
            //HotelRateDescriptionService hotelRateDescriptionService = new HotelRateDescriptionService();
            //var rq = new HotelRateDescriptionRQ()
            //{
            //    AvailRequestSegment = new HotelRateDescriptionRQAvailRequestSegment()
            //    {
            //        GuestCounts = new HotelRateDescriptionRQAvailRequestSegmentGuestCounts() { Count = "2" }
            //,
            //        HotelSearchCriteria = new HotelRateDescriptionRQAvailRequestSegmentHotelSearchCriteria() { Criterion = new HotelRateDescriptionRQAvailRequestSegmentHotelSearchCriteriaCriterion() { HotelRef = new HotelRateDescriptionRQAvailRequestSegmentHotelSearchCriteriaCriterionHotelRef() { HotelCode = "1" } } }
            //,
            //        RatePlanCandidates = new HotelRateDescriptionRQAvailRequestSegmentRatePlanCandidates() { RatePlanCandidate = new HotelRateDescriptionRQAvailRequestSegmentRatePlanCandidatesRatePlanCandidate() { CurrencyCode = "USD", DCA_ProductCode = "A1B2C3D" } },
            //        TimeSpan = new HotelRateDescriptionRQAvailRequestSegmentTimeSpan() { End = "1-31", Start = "2-25" }
            //    }
            //};
            //string s = JsonConvert.SerializeObject(rq);
            //HotelRateDescriptionRS hotelRateDescriptionRS = hotelRateDescriptionService.HotelRateDescriptionRQ(rq);

            //// 步骤4:添加额外的(必需的)信息来创建乘客记录(PNR)
            //PassengerDetailsService passengerDetailsService = new PassengerDetailsService();
            //var rq = new PassengerDetailsRQ()
            //{
            //    PostProcessing = new PassengerDetailsRQPostProcessing() { IgnoreAfter = false, RedisplayReservation = true, UnmaskCreditCard = true }
            //,
            //    PreProcessing = new PassengerDetailsRQPreProcessing() { IgnoreBefore = true, UniqueID = new PassengerDetailsRQPreProcessingUniqueID() { ID = "" } }
            //,
            //    SpecialReqDetails = new PassengerDetailsRQSpecialReqDetails()
            //    {
            //        SpecialServiceRQ = new PassengerDetailsRQSpecialReqDetailsSpecialServiceRQ()
            //        {
            //            SpecialServiceInfo = new PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfo()
            //            {
            //                AdvancePassenger = new PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfoAdvancePassenger[]
            //{ new PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfoAdvancePassenger() { SegmentNumber = "A" ,Document=new PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfoAdvancePassengerDocument() { ExpirationDate=DateTime.Parse("2019-05-26"),Number="18382099231",Type="P",IssueCountry="FR",NationalityCountry="FR"}
            //,PersonName=new PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfoAdvancePassengerPersonName() { DateOfBirth=DateTime.Parse("1980-12-02"),Gender=PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfoAdvancePassengerPersonNameGender.M,NameNumber="1.1",DocumentHolder=true,GivenName="JAMES",MiddleName="RS",Surname="GREEN"}
            //,VendorPrefs=new PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfoAdvancePassengerVendorPrefs() { Airline=new PassengerDetailsRQSpecialReqDetailsSpecialServiceRQSpecialServiceInfoAdvancePassengerVendorPrefsAirline() { Hosted=false} } } }
            //            }
            //        }
            //    }
            //,
            //    TravelItineraryAddInfoRQ = new PassengerDetailsRQTravelItineraryAddInfoRQ()
            //    {
            //        AgencyInfo = new PassengerDetailsRQTravelItineraryAddInfoRQAgencyInfo()
            //        {
            //            Address = new PassengerDetailsRQTravelItineraryAddInfoRQAgencyInfoAddress()
            //            {
            //                AddressLine = "SABRE TRAVEL",
            //                CityName = "SOUTHLAKE",
            //                CountryCode = "US",
            //                PostalCode = "76092"
            //,
            //                StateCountyProv = new PassengerDetailsRQTravelItineraryAddInfoRQAgencyInfoAddressStateCountyProv() { StateCode = "TX" },
            //                StreetNmbr = "3150 SABRE DRIVE",
            //                VendorPrefs = new PassengerDetailsRQTravelItineraryAddInfoRQAgencyInfoAddressVendorPrefs() { Airline = new PassengerDetailsRQTravelItineraryAddInfoRQAgencyInfoAddressVendorPrefsAirline() { Hosted = false } }
            //            }
            //        }
            //,
            //        CustomerInfo = new PassengerDetailsRQTravelItineraryAddInfoRQCustomerInfo()
            //        {
            //            ContactNumbers = new PassengerDetailsRQTravelItineraryAddInfoRQCustomerInfoContactNumber[1] { new PassengerDetailsRQTravelItineraryAddInfoRQCustomerInfoContactNumber() { NameNumber = "1.1", Phone = "682-605-1000", PhoneUseType = "H" } }
            //,
            //            PersonName = new PassengerDetailsRQTravelItineraryAddInfoRQCustomerInfoPersonName[1] { new PassengerDetailsRQTravelItineraryAddInfoRQCustomerInfoPersonName() { NameNumber = "1.1", NameReference = "NameReference", PassengerType = "ADT", GivenName = "Ram", Surname = "Jee" } }
            //        }
            //    }
            //};

            //string s = JsonConvert.SerializeObject(rq);
            //PassengerDetailsRS passengerDetailsRS = passengerDetailsService.PassengerDetailsRQ(rq);

            //// 第五步:为选定的酒店预订房间
            //OTA_HotelResService hotelResService = new OTA_HotelResService();
            //var rq = new OTA_HotelResRQ()
            //{
            //    Hotel = new OTA_HotelResRQHotel()
            //    {
            //        BasicPropertyInfo = new OTA_HotelResRQHotelBasicPropertyInfo() { RPH = "2" },
            //        Guarantee = new OTA_HotelResRQHotelGuarantee()
            //        {
            //            Type = "GDPST",
            //            CC_Info = new OTA_HotelResRQHotelGuaranteeCC_Info()
            //            {
            //                PaymentCard = new OTA_HotelResRQHotelGuaranteeCC_InfoPaymentCard() { Code = "AX", ExpireDate = "2019-12", Number = "1234567890" }
            //,
            //                PersonName = new OTA_HotelResRQHotelGuaranteeCC_InfoPersonName() { Surname = "TEST" }
            //            }
            //        },
            //        RoomType = new OTA_HotelResRQHotelRoomType() { NumberOfUnits = "1" },
            //        SpecialPrefs = new OTA_HotelResRQHotelSpecialPrefs() { WrittenConfirmation = new OTA_HotelResRQHotelSpecialPrefsWrittenConfirmation() { Ind = true } }
            //    }
            //};
            //string s = JsonConvert.SerializeObject(rq);
            //OTA_HotelResRS hotelResRS = hotelResService.OTA_HotelResRQ(rq);

            //// 步骤6:结束乘客姓名记录的事务
            //EndTransactionService endTransactionService = new EndTransactionService();
            //var rq = new EndTransactionRQ() { EndTransaction = new EndTransactionRQEndTransaction() { Ind = true }, Source = new EndTransactionRQSource() { ReceivedFrom = "SWS TEST" } };
            //string s = JsonConvert.SerializeObject(rq);
            //EndTransactionRS endTransactionRS = endTransactionService.EndTransactionRQ(rq);

            Console.ReadLine();
        }

        public static void AddSerializedResultXML(HotelChain i)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(HotelChain));
            XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };
            using (StringWriter writer = new StringWriter())
            {
                using (XmlWriter xmlWriter = XmlWriter.Create(writer, settings))
                {
                    serializer.Serialize(xmlWriter, i);
                    string s = writer.ToString();
                }
            }

        }

        public static MessageHeader CreateHeader(string action, string conversationId)
        {
            string pseudoCityCode = configProvider.Group;

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

        public static Security CreateUsernameSecurity()
        {
            string userName = configProvider.UserId;
            string password = configProvider.ClientSecret;
            string domain = configProvider.Domain;
            string pseudoCityCode = configProvider.Group;

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

        public static SessionCreateRQ CreateSessionCreateRequest()
        {
            return new SessionCreateRQ
            {
                POS = new SessionCreateRQPOS
                {
                    Source = new SessionCreateRQPOSSource
                    {
                        PseudoCityCode = configProvider.Group
                    }
                }
            };
        }
    }

    [XmlRoot("HotelChain")]
    public class HotelChain
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("Source")]
        public LinksItem[] Links { get; set; }
    }

    public class LinksItem
    {
        [XmlAttribute("rel")]
        public string rel { get; set; }

        [XmlAttribute("href")]
        public string href { get; set; }

        [XmlText]
        public string content { get; set; }
    }

    public static class SoapHelper
    {
        /// <summary>
        /// Handles the errors in asynchronous calls. 
        /// If an error has occurred, sets a result in the task completion source containing information about the problem:
        /// either the Exception that has occurred or the cancellation flag.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="args">The <see cref="AsyncCompletedEventArgs"/> instance containing the event data.</param>
        /// <param name="source">The <see cref="TaskCompletionSource"/> instance related to the SOAP result.</param>
        /// <returns><c>true</c> if no errors have occurred; otherwise <c>false</c></returns>
        public static bool HandleErrors<TResult>(AsyncCompletedEventArgs args, TaskCompletionSource<SoapResult<TResult>> source)
        {
            if (args.Error != null)
            {
                source.TrySetResult(SoapResult<TResult>.Error(args.Error));
                return false;
            }
            else if (args.Cancelled)
            {
                source.TrySetResult(SoapResult<TResult>.Cancelled());
                return false;
            }

            return true;
        }

    }

    public class SoapResult<TResult>
    {
        /// <summary>
        /// Gets the security token.
        /// </summary>
        /// <value>
        /// The security token.
        /// </value>
        public Security Security { get; private set; }

        /// <summary>
        /// Gets the result (response model).
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public TResult Result { get; private set; }

        /// <summary>
        /// Gets the exception that has occurred during request.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the request was cancelled.
        /// </summary>
        /// <value>
        /// <c>true</c> if the request was cancelled; otherwise, <c>false</c>.
        /// </value>
        public bool IsCancelled { get; private set; }

        /// <summary>
        /// Gets the type of the error.
        /// </summary>
        /// <value>
        /// The type of the error.
        /// </value>
        public string ErrorType { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the request has succeeded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the request has succeeded; otherwise, <c>false</c>.
        /// </value>
        public bool IsOk
        {
            get { return !this.IsCancelled && !this.ErrorOccurred; }
        }

        /// <summary>
        /// Gets a value indicating whether an error has occurred. 
        /// If <c>true</c>, then either Exception or ErrorType is set.
        /// </summary>
        /// <value>
        ///   <c>true</c> if an error has occurred; otherwise, <c>false</c>.
        /// </value>
        public bool ErrorOccurred
        {
            get { return this.Exception != null || this.ErrorType != null; }
        }

        /// <summary>
        /// Creates a new success SOAP result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <param name="security">The security token.</param>
        /// <returns>The SOAP result.</returns>
        public static SoapResult<TResult> Success(TResult result, Security security)
        {
            return new SoapResult<TResult>
            {
                Result = result,
                Security = security
            };
        }

        /// <summary>
        /// Creates a new success SOAP result.
        /// </summary>
        /// <param name="result">The result.</param>
        /// <returns>The SOAP result.</returns>
        public static SoapResult<TResult> Success(TResult result)
        {
            return new SoapResult<TResult>
            {
                Result = result
            };
        }

        /// <summary>
        /// Creates a new error SOAP result.
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns>The SOAP result.</returns>
        public static SoapResult<TResult> Error(Exception exception)
        {
            return new SoapResult<TResult>
            {
                Exception = exception
            };
        }

        /// <summary>
        /// Creates a new cancelled SOAP result.
        /// </summary>
        /// <returns>The SOAP result.</returns>
        public static SoapResult<TResult> Cancelled()
        {
            return new SoapResult<TResult>
            {
                IsCancelled = true
            };
        }

        /// <summary>
        /// Creates a new SOAP result with an error type.
        /// </summary>
        /// <param name="errorType">Type of the error.</param>
        /// <returns>The SOAP result.</returns>
        public static SoapResult<TResult> Error(string errorType)
        {
            return new SoapResult<TResult>
            {
                ErrorType = errorType
            };
        }
    }
}
