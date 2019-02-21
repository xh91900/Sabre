using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
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
            Maticsoft.DBUtility.DbHelperOra.connectionString = "Data Source=orcl;User Id=hotel;Password=yeesky8848";
            string sql = "select distinct(t.encode) from SABRE_CODEBASE t where  t.code_group='CITY'";
            DataSet ds= Maticsoft.DBUtility.DbHelperOra.Query(sql);
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string cityCode = ds.Tables[0].Rows[i]["encode"].ToString();

                    var security = this.sessionPool.TakeSessionAsync(sharedContext.ConversationId);
                    OTA_HotelAvailLLSRQApi.Security1 security1 = new OTA_HotelAvailLLSRQApi.Security1() { BinarySecurityToken = security.BinarySecurityToken };
                    var service = this.soapServiceFactory.CreateOTA_HotelAvailService(sharedContext.ConversationId, security1);
                    var request = this.CreateRequest(cityCode);

                    try
                    {
                        var result = service.OTA_HotelAvailRQ(request);

                        if (result.ApplicationResults.status == OTA_HotelAvailLLSRQApi.CompletionCodes.Complete && result.ApplicationResults.Error == null)
                        {
                            for (int j = 0; j < result.AvailabilityOptions.Count(); j++)
                            {
                                var option = result.AvailabilityOptions[j];
                                string address = string.Join(",", option.BasicPropertyInfo.Address);

                                sql = @"insert into SABRE_HOTEL (HotelCode,HotelName,HotelCityCode,AreaID,inserttime,updatetime,ChainCode,Distance,GEO_ConfidenceLevel,
Latitude,Longitude,HotelRateCode,RateLevelCode,LocationDescription,Fax,Phone,Address) values(:HotelCode,:HotelName,:HotelCityCode,:AreaID,:inserttime,:updatetime,:ChainCode,:Distance,:GEO_ConfidenceLevel,
:Latitude,:Longitude,:HotelRateCode,:RateLevelCode,:LocationDescription,:Fax,:Phone,:Address)";
                                OracleParameter[] parameters = {
                    new OracleParameter(":HotelCode", OracleType.NVarChar,20),
                    new OracleParameter(":HotelName", OracleType.NVarChar,100),
                    new OracleParameter(":HotelCityCode", OracleType.NVarChar,200),
                    new OracleParameter(":AreaID", OracleType.NVarChar,20),
                    new OracleParameter(":inserttime", OracleType.DateTime),
                    new OracleParameter(":updatetime", OracleType.DateTime),
                    new OracleParameter(":ChainCode", OracleType.NVarChar,20),
                    new OracleParameter(":Distance", OracleType.NVarChar,20),
                    new OracleParameter(":GEO_ConfidenceLevel", OracleType.NVarChar,30),
                    new OracleParameter(":Latitude", OracleType.NVarChar,20),
                    new OracleParameter(":Longitude", OracleType.NVarChar,30),
                    new OracleParameter(":HotelRateCode", OracleType.NVarChar,100),
                    new OracleParameter(":RateLevelCode", OracleType.NVarChar,200),
                    new OracleParameter(":LocationDescription", OracleType.NVarChar,200),
                    new OracleParameter(":Fax", OracleType.NVarChar,100),
                    new OracleParameter(":Phone", OracleType.NVarChar,20),
                    new OracleParameter(":Address", OracleType.NVarChar,200)};

                                parameters[0].Value = option.BasicPropertyInfo.HotelCode;
                                parameters[1].Value = option.BasicPropertyInfo.HotelName;
                                parameters[2].Value = option.BasicPropertyInfo.HotelCityCode;
                                parameters[3].Value = option.BasicPropertyInfo.AreaID;
                                parameters[4].Value = DateTime.Now;
                                parameters[5].Value = DateTime.Now;
                                parameters[6].Value = option.BasicPropertyInfo.ChainCode;
                                parameters[7].Value = option.BasicPropertyInfo.Distance;
                                parameters[8].Value = option.BasicPropertyInfo.GEO_ConfidenceLevel;
                                parameters[9].Value = option.BasicPropertyInfo.Latitude;
                                parameters[10].Value = option.BasicPropertyInfo.Longitude;
                                parameters[11].Value = "";
                                parameters[12].Value = "";
                                parameters[13].Value = option.BasicPropertyInfo.LocationDescription.Text;
                                parameters[14].Value = option.BasicPropertyInfo.ContactNumbers.ContactNumber.Fax;
                                parameters[15].Value = option.BasicPropertyInfo.ContactNumbers.ContactNumber.Phone;
                                parameters[16].Value = string.Join(",", option.BasicPropertyInfo.Address);
                                int rows = Maticsoft.DBUtility.DbHelperOra.ExecuteSql(sql, parameters);
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        // 日志
                        sharedContext.IsFaulty = true;
                        this.sessionPool.ReleaseSession(sharedContext.ConversationId);
                        return null;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// 获取请求参数对象
        /// </summary>
        /// <returns></returns>
        public OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQ CreateRequest(string cityCode)
        {
            return new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQ()
            {
                AvailRequestSegment = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegment()
                {
                    Customer = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentCustomer()
                    {
                        //Corporate = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentCustomerCorporate() { ID = "ABC123" }
                    },
                    GuestCounts = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentGuestCounts() { Count = "2" },
                    HotelSearchCriteria = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteria()
                    {
                        NumProperties = "100",
                        Criterion = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteriaCriterion()
                        {
                            HotelRef = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteriaCriterionHotelRef[1]
                        { new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentHotelSearchCriteriaCriterionHotelRef() { HotelCityCode = cityCode } }
                        }
                    },
                    TimeSpan = new OTA_HotelAvailLLSRQApi.OTA_HotelAvailRQAvailRequestSegmentTimeSpan()
                    {
                        Start = "02-26",
                        End = "02-28"
                    }
                },
                Version = "2.3.0"
            };
        }
    }
}
