using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo.soap
{
    /// <summary>
    /// 获取酒店图像 api 
    /// </summary>
    public class GetHotelImage : IActivity
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
        public GetHotelImage(SoapServiceFactory soapServiceFactory, SessionPool sessionPool)
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
            string[] categoryCode = new string[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20", "21", "22", "23" };

            Maticsoft.DBUtility.DbHelperOra.connectionString = "Data Source=orcl;User Id=hotel;Password=yeesky8848";
            string sql = "select count(1) from sabre_hotel";
            long totalCount = long.Parse(Maticsoft.DBUtility.DbHelperOra.Query(sql).Tables[0].Rows[0][0].ToString());

            int pageIndex = (int)Math.Ceiling(decimal.Parse((totalCount / 300).ToString()));

            for (int o = 0; 0 < pageIndex; o++)
            {
                long pageStart = o * 300;
                long pageEnd = (o + 1) * 300;
                sql = "select hotelcode from (select a1.hotelcode,rownum rn from sabre_hotel a1 where rownum <" + pageEnd + ") where rn>=" + pageStart + "";
                DataSet ds = Maticsoft.DBUtility.DbHelperOra.Query(sql);
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    List<GetHotelImageRQApi.HotelRef> hotelCodeList = new List<GetHotelImageRQApi.HotelRef>();

                    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                    {
                        string hotelCode = ds.Tables[0].Rows[i]["hotelcode"].ToString();
                        hotelCodeList.Add(new GetHotelImageRQApi.HotelRef() { HotelCode = hotelCode, CodeContext = "Sabre" });
                    }
                    for (int i = 0; i < categoryCode.Length; i++)
                    {
                        var security = this.sessionPool.TakeSessionAsync(sharedContext.ConversationId);
                        GetHotelImageRQApi.Security security1 = new GetHotelImageRQApi.Security()
                        { BinarySecurityToken = security.BinarySecurityToken, Actor = security.Actor };
                        var service = this.soapServiceFactory.CreateGetHotelImageService(sharedContext.ConversationId, security1);

                        var request = this.CreateRequest(categoryCode[i], hotelCodeList);
                        try
                        {
                            Helper h = new Helper();
                            //string s = h.ToXml<GetHotelImageRQApi.MessageHeader>(service.MessageHeaderValue);
                            //string s2 = h.ToXml<GetHotelImageRQApi.Security>(service.SecurityValue);
                            //string s1 = h.ToXml<GetHotelImageRQApi.GetHotelImageRQ>(request);

                            var result = service.GetHotelImageRQ(request);

                            if (result.ApplicationResults.status == GetHotelImageRQApi.CompletionCodes.Complete && result.HotelImageInfos.HotelImageInfo != null)
                            {
                                for (int j = 0; j < result.HotelImageInfos.HotelImageInfo.Length; j++)
                                {
                                    var HotelImageInfo = result.HotelImageInfos.HotelImageInfo[j];
                                    string imgJson = Newtonsoft.Json.JsonConvert.SerializeObject(result.HotelImageInfos);
                                    sql = @"insert into SABRE_IMAGE (HOTELID,CATEGORYCODE,IMAGEID,IMAGEURL,IMAGEHEIGHT,IMAGEWIDTH,INSERTTIME,UPDATETIME)
values(:HOTELID,:CATEGORYCODE,:IMAGEID,:IMAGEURL,:IMAGEHEIGHT,:IMAGEWIDTH,:INSERTTIME,:UPDATETIME)";
                                    OracleParameter[] parameters = {
                    new OracleParameter(":HOTELID", OracleType.NVarChar,20),
                    new OracleParameter(":CATEGORYCODE", OracleType.NVarChar,5),
                    new OracleParameter(":IMAGEID", OracleType.NVarChar,20),
                    new OracleParameter(":IMAGEURL", OracleType.NVarChar,200),
                    new OracleParameter(":IMAGEHEIGHT", OracleType.Number),
                    new OracleParameter(":IMAGEWIDTH", OracleType.Number),
                    new OracleParameter(":INSERTTIME", OracleType.DateTime),
                    new OracleParameter(":UPDATETIME", OracleType.DateTime) };
                                    parameters[0].Value = HotelImageInfo.HotelInfo.HotelCode;
                                    parameters[1].Value = HotelImageInfo.ImageItem.Category.CategoryCode;
                                    parameters[2].Value = HotelImageInfo.ImageItem.Id;
                                    parameters[3].Value = HotelImageInfo.ImageItem.Image.Url;
                                    parameters[4].Value = HotelImageInfo.ImageItem.Image.Height;
                                    parameters[5].Value = HotelImageInfo.ImageItem.Image.Width;
                                    parameters[6].Value = DateTime.Now;
                                    parameters[7].Value = HotelImageInfo.ImageItem.LastModifedDate;
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
            }


            return null;
        }

        /// <summary>
        /// 获取请求参数对象
        /// </summary>
        /// <returns></returns>
        public GetHotelImageRQApi.GetHotelImageRQ CreateRequest(string categoryCode, List<GetHotelImageRQApi.HotelRef> hotelCodeList)
        {
            return new GetHotelImageRQApi.GetHotelImageRQ()
            {
                HotelRefs = new GetHotelImageRQApi.HotelRefs()
                {
                    //HotelRef = new GetHotelImageRQApi.HotelRef[] { },
                    HotelRef = hotelCodeList.ToArray(),
                },
                ImageRef = new GetHotelImageRQApi.ImageRef() { Type = GetHotelImageRQApi.ImageTypes.LARGE, CategoryCode = categoryCode, LanguageCode = "CN" },
                version = "1.0.0"
            };
        }
    }
}
