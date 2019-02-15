using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo.model.soap.PassengerDetailsRQ
{
    /// <summary>
    /// 步骤4:添加额外的(必需的)信息来创建乘客记录(PNR)
    /// </summary>
    public class PassengerDetailsRQ
    {
        /// <summary>
        /// 用于指定与杂项段关联的信息【obj】
        /// </summary>
        public string MiscSegmentSellRQ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PostProcessing PostProcessing { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public PreProcessing PreProcessing { get; set; }
        /// <summary>
        /// 用于将先前增强型“AirbookRQ”或增强型“AirbookWithTaxRQ”存储的报价记录与乘客姓名在PassengerDetailsRQ中关联。
        /// </summary>
        public string PriceQuoteInfo { get; set; }
        /// <summary>
        /// 用于指定要移动到AAA中以创建PNR的配置文件名。
        /// </summary>
        public string ProfileRQ { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public SpecialReqDetails SpecialReqDetails { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public TravelItineraryAddInfoRQ TravelItineraryAddInfoRQ { get; set; }
        /// <summary>
        /// (if true)如果低级调用返回错误，则通知系统停止处理
        /// </summary>
        public string HaltOnErrorSpecified { get; set; }
        /// <summary>
        /// (if true)通知系统在遇到任何错误时忽略整个事务。 
        /// </summary>
        public string IgnoreOnErrorSpecified { get; set; }
        /// <summary>
        /// 版本
        /// </summary>
        public string version { get; set; }
    }

    public class PostProcessing
    {
        /// <summary>
        /// 用于将到达未知段添加到乘客姓名记录中。系统将正确地应用它们，因此不需要进行分段选择。
        /// </summary>
        public string ARUNK_RQ { get; set; }
        /// <summary>
        /// 用于结束事务并完成记录。【obj】
        /// </summary>
        public string EndTransactionRQ { get; set; }
        /// <summary>
        /// 队列配置【obj】
        /// </summary>
        public string QueuePlaceRQ { get; set; }
        /// <summary>
        /// 用于执行忽略以清除AAA。请注意，它不会关闭会话。
        /// </summary>
        public string IgnoreAfterSpecified { get; set; }
        /// <summary>
        /// 用于指示是否重新显示PNR。
        /// </summary>
        public string RedisplayReservationSpecified { get; set; }
        /// <summary>
        /// 用于在TIR响应中取消对信用卡信息的屏蔽。请注意，只有当用户具有epr关键字ccview时，此属性才有效。
        /// </summary>
        public string UnmaskCreditCard { get; set; }
    }

    /// <summary>
    /// 用于指定要排队放置的记录定位器
    /// </summary>
    public class UniqueID
    {
        /// <summary>
        /// 用于指定要排队放置的记录定位器
        /// </summary>
        public string ID { get; set; }
    }

    public class PreProcessing
    {
        /// <summary>
        /// 用于指定要排队放置的记录定位器
        /// </summary>
        public UniqueID UniqueID { get; set; }
        /// <summary>
        /// 用于执行忽略以清除AAA。请注意，它不会关闭会话
        /// </summary>
        public string IgnoreBeforeSpecified { get; set; }
    }

    public class Document
    {
        /// <summary>
        /// 签发国
        /// </summary>
        public string IssueCountry { get; set; }
        /// <summary>
        /// 国家/地区
        /// </summary>
        public string NationalityCountry { get; set; }
        /// <summary>
        /// 签证
        /// </summary>
        public string Visa { get; set; }
        /// <summary>
        /// 用于指定文档过期日期
        /// YYYY-MM-DD
        /// </summary>
        public string ExpirationDateSpecified { get; set; }
        /// <summary>
        /// 用于指定文档编号。
        /// </summary>
        public string Number { get; set; }
        /// <summary>
        /// 用于指定文档类型。
        /// 可接受值包括：“A”-外籍居民卡，“C”-永久居民卡，“F”-便利文件，“I”-国家身份证，“In”-Nexus卡。
        /// </summary>
        public string Type { get; set; }
    }

    public class PersonName
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string GivenName { get; set; }
        /// <summary>
        /// 中间名
        /// </summary>
        public string MiddleName { get; set; }
        /// <summary>
        /// 姓
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// YYYY-MM-DD 
        /// YY-MM-DD
        /// </summary>
        public string DateOfBirthSpecified { get; set; }
        /// <summary>
        /// 用于在为多名乘客签发护照文件时识别主要护照持有人
        /// </summary>
        public string DocumentHolderSpecified { get; set; }
        /// <summary>
        /// M
        /// F
        /// </summary>
        public string GenderSpecified { get; set; }
        /// <summary>
        /// 用于指定乘客姓名编号
        /// </summary>
        public string NameNumber { get; set; }
    }

    public class Airline
    {
        /// <summary>
        /// 用于指定请求是基于承载的还是非承载的运营商，并且是必需的。
        /// </summary>
        public string HostedSpecified { get; set; }
    }

    public class VendorPrefs
    {
        /// <summary>
        /// 用于指定集团正在旅行的航空公司（如果适用）。
        /// </summary>
        public Airline Airline { get; set; }
    }

    public class AdvancePassengerItem
    {
        /// <summary>
        /// 证件信息
        /// </summary>
        public Document Document { get; set; }
        /// <summary>
        /// 客人信息
        /// </summary>
        public PersonName PersonName { get; set; }
        /// <summary>
        /// 住所地址【obj】
        /// </summary>
        public string ResidentDestinationAddress { get; set; }
        /// <summary>
        /// 【obj】
        /// </summary>
        public VendorPrefs VendorPrefs { get; set; }
        /// <summary>
        /// 用于指定与SSR关联的行程段。“可以指定“将SSR与所有段关联。
        /// </summary>
        public string SegmentNumber { get; set; }
    }

    public class SpecialServiceInfo
    {
        /// <summary>
        /// 顾客
        /// </summary>
        public List<AdvancePassengerItem> AdvancePassenger { get; set; }
        /// <summary>
        /// 【obj】
        /// </summary>
        public string SecureFlight { get; set; }
        /// <summary>
        /// 服务【obj】
        /// </summary>
        public string Service { get; set; }
    }

    public class SpecialServiceRQ
    {
        /// <summary>
        /// 服务
        /// </summary>
        public SpecialServiceInfo SpecialServiceInfo { get; set; }
    }

    public class SpecialReqDetails
    {
        /// <summary>
        /// 备注信息【obj】
        /// </summary>
        public string AddRemarkRQ { get; set; }
        /// <summary>
        /// 座位信息【obj】
        /// </summary>
        public string AirSeatRQ { get; set; }
        /// <summary>
        /// 服务
        /// </summary>
        public SpecialServiceRQ SpecialServiceRQ { get; set; }
    }

    public class StateCountyProv
    {
        /// <summary>
        /// TX
        /// </summary>
        public string StateCode { get; set; }
    }

    public class Address
    {
        /// <summary>
        /// 地址线
        /// </summary>
        public string AddressLine { get; set; }
        /// <summary>
        /// 城市名
        /// </summary>
        public string CityName { get; set; }
        /// <summary>
        /// 国家代码
        /// </summary>
        public string CountryCode { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// 【obj】
        /// </summary>
        public StateCountyProv StateCountyProv { get; set; }
        /// <summary>
        /// 街道号
        /// </summary>
        public string StreetNmbr { get; set; }
        /// <summary>
        /// 【obj】
        /// </summary>
        public VendorPrefs VendorPrefs { get; set; }
    }

    /// <summary>
    /// 代理信息
    /// </summary>
    public class AgencyInfo
    {
        /// <summary>
        /// 地址信息
        /// </summary>
        public Address Address { get; set; }
        /// <summary>
        /// 票信息【obj】
        /// </summary>
        public string Ticketing { get; set; }
    }

    public class ContactNumbersItem
    {
        /// <summary>
        /// 用于在现有电话号码之后或之前添加电话号码
        /// </summary>
        public string InsertAfter { get; set; }
        /// <summary>
        /// 用于覆盖应用程序的出差日记帐记录中的默认城市
        /// </summary>
        public string LocationCode { get; set; }
        /// <summary>
        /// 用于指定乘客姓名编号
        /// </summary>
        public string NameNumber { get; set; }
        /// <summary>
        /// 用于在记录中添加客户电话号码（如果适用）。
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// 用于指定号码是“A”、“Home”、“H”、“Business”、“B”还是“Fax”、“F”。
        /// </summary>
        public string PhoneUseType { get; set; }
    }

    public class PersonNameItem
    {
        /// <summary>
        /// 名字
        /// </summary>
        public string GivenName { get; set; }
        /// <summary>
        /// 组信息【obj】
        /// </summary>
        public string GroupInfo { get; set; }
        /// <summary>
        /// 姓
        /// </summary>
        public string Surname { get; set; }
        /// <summary>
        /// 用于说明特定乘客是婴儿
        /// </summary>
        public string InfantSpecified { get; set; }
        /// <summary>
        /// 用于指定乘客姓名编号，仅当与@passengertype一起使用时才适用。
        /// </summary>
        public string NameNumber { get; set; }
        /// <summary>
        /// 用于指定其他名称引用相关信息（如果适用）。
        /// </summary>
        public string NameReference { get; set; }
        /// <summary>
        /// 用于在记录中添加乘客类型代码。请注意，此限定符不适用于基于Sabre Sonic Res的客户。
        /// </summary>
        public string PassengerType { get; set; }
    }

    /// <summary>
    /// 客户信息
    /// </summary>
    public class CustomerInfo
    {
        /// <summary>
        /// 联系人信息
        /// </summary>
        public List<ContactNumbersItem> ContactNumbers { get; set; }
        /// <summary>
        /// 用于在记录中添加公司ID（如果适用）。请注意，此限定符不适用于基于Sabre Sonic Res的客户。【obj】
        /// </summary>
        public string Corporate { get; set; }
        /// <summary>
        /// 用于在记录中添加经常旅客信息【obj】
        /// </summary>
        public string CustLoyalty { get; set; }
        /// <summary>
        /// 客户识别
        /// </summary>
        public string CustomerIdentifier { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<PersonNameItem> PersonName { get; set; }
    }

    public class TravelItineraryAddInfoRQ
    {
        /// <summary>
        /// obj
        /// </summary>
        public AgencyInfo AgencyInfo { get; set; }
        /// <summary>
        /// obj
        /// </summary>
        public CustomerInfo CustomerInfo { get; set; }
    }
}
