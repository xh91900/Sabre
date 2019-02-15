using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo.model.soap
{
    /// <summary>
    /// 第五步:为选定的酒店预订房间
    /// 用于预订一个或多个酒店客房
    /// </summary>
    public class OTA_HotelResRQ
    {
        /// <summary>
        /// 
        /// </summary>
        public Hotel Hotel { get; set; }
        /// <summary>
        /// 指定是否在响应消息中返回本机主机命令
        /// </summary>
        public string ReturnHostCommandSpecified { get; set; }
        /// <summary>
        /// 时间戳
        /// </summary>
        public string TimeStampSpecified { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string Version { get; set; }
    }

    public class BasicPropertyInfo
    {
        /// <summary>
        /// 用于指定酒店确认号
        /// </summary>
        public string ConfirmationNumber { get; set; }
        /// <summary>
        /// 连锁代码
        /// </summary>
        public string ChainCode { get; set; }
        /// <summary>
        /// 酒店代码
        /// </summary>
        public string HotelCode { get; set; }
        /// <summary>
        /// 用于指定放置新酒店段的现有段
        /// </summary>
        public string InsertAfter { get; set; }
        /// <summary>
        /// 用于在要从中销售的hotelpropertydescriptionllsrq/hotelratedescriptionllsrq响应中指定行号。
        /// </summary>
        public string RPH { get; set; }
    }

    public class PaymentCard
    {
        /// <summary>
        /// 用于指定信用卡代码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 用于指定信用卡到期日期
        /// </summary>
        public string ExpireDate { get; set; }
        /// <summary>
        /// 用于指定信用卡号
        /// </summary>
        public string Number { get; set; }
    }

    public class PersonName
    {
        /// <summary>
        /// 用于指定持卡人的姓氏
        /// </summary>
        public string Surname { get; set; }
    }

    public class CC_Info
    {
        /// <summary>
        /// 信用卡信息【obj】
        /// </summary>
        public PaymentCard PaymentCard { get; set; }
        /// <summary>
        /// 持卡人信息【obj】
        /// </summary>
        public PersonName PersonName { get; set; }
    }

    public class Guarantee
    {
        /// <summary>
        /// 信用卡信息
        /// </summary>
        public CC_Info CC_Info { get; set; }
        /// <summary>
        /// 用于指定其他特殊首选项信息（如果适用）。
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// "G", "GAGT", "GDPST", "GC", "GCR", "GH", "GDPSTH", "GT", or "GDPSTT"
        /// </summary>
        public string Type { get; set; }
    }

    public class RoomType
    {
        /// <summary>
        /// 用于指定房间数
        /// </summary>
        public string NumberOfUnits { get; set; }
        /// <summary>
        /// 用于指定酒店价格代码
        /// </summary>
        public string RoomTypeCode { get; set; }
    }

    public class WrittenConfirmation
    {
        /// <summary>
        /// 用于向供应商请求书面预订确认
        /// </summary>
        public string Ind { get; set; }
    }

    public class SpecialPrefs
    {
        /// <summary>
        /// 用于指定其他特殊首选项信息（如果适用）。
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// 用于向供应商请求书面预订确认
        /// </summary>
        public WrittenConfirmation WrittenConfirmation { get; set; }
    }

    public class Hotel
    {
        /// <summary>
        /// 
        /// </summary>
        public BasicPropertyInfo BasicPropertyInfo { get; set; }
        /// <summary>
        /// 客户信息【obj】
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// is no longer in use
        /// </summary>
        public string DCA_LongSell { get; set; }
        /// <summary>
        /// 【obj】
        /// </summary>
        public Guarantee Guarantee { get; set; }
        /// <summary>
        /// 用于指定房间占用人数-最多2人【obj】
        /// </summary>
        public string GuestCounts { get; set; }
        /// <summary>
        /// POS
        /// </summary>
        public string POS { get; set; }
        /// <summary>
        /// 用于指定合同或协商价格【obj】
        /// </summary>
        public string RatePlanCandidates { get; set; }
        /// <summary>
        /// 【obj】
        /// </summary>
        public RoomType RoomType { get; set; }
        /// <summary>
        /// 【obj】
        /// </summary>
        public SpecialPrefs SpecialPrefs { get; set; }
        /// <summary>
        /// 时间范围【obj】
        /// </summary>
        public string TimeSpan { get; set; }
    }
}
