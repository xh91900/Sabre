using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo.model.soap.OTA_HotelAvailRQ
{
    /// <summary>
    /// 步骤1:检索酒店可用性
    /// </summary>
    public class OTA_HotelAvailRQ
    {
        /// <summary>
        /// 检索条件
        /// </summary>
        public AvailRequestSegment AvailRequestSegment { get; set; }
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

    /// <summary>
    /// 公司信息
    /// </summary>
    public class Corporate
    {
        /// <summary>
        /// 公司ID
        /// </summary>
        public string ID { get; set; }
    }

    /// <summary>
    /// 客户信息
    /// </summary>
    public class Customer
    {
        /// <summary>
        /// 公司信息
        /// </summary>
        public Corporate Corporate { get; set; }
        /// <summary>
        /// 用于指定ID号（如果适用）
        /// </summary>
        public string ID { get; set; }
    }

    /// <summary>
    /// 客人数量
    /// </summary>
    public class GuestCounts
    {
        /// <summary>
        /// 用于指定房间居住人数-最多9人
        /// </summary>
        public string Count { get; set; }
    }

    /// <summary>
    /// 酒店信息
    /// 如果客户希望根据多个酒店连锁代码进行查询
    /// 如果客户希望查询多个酒店代码
    /// </summary>
    public class HotelRefItem
    {
        /// <summary>
        /// 连锁代码
        /// </summary>
        public string ChainCode { get; set; }
        /// <summary>
        /// 用于覆盖销售点位置的默认搜索度量
        /// </summary>
        public string UnitOfMeasureSpecified { get; set; }
        /// <summary>
        /// 城市代码
        /// </summary>
        public string HotelCityCode { get; set; }
        /// <summary>
        /// 酒店代码
        /// </summary>
        public string HotelCode { get; set; }
        /// <summary>
        /// 酒店名称
        /// </summary>
        public string HotelName { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string Longitude { get; set; }
    }

    /// <summary>
    /// 检索范围
    /// </summary>
    public class Criterion
    {
        /// <summary>
        /// 地址信息【obj】
        /// </summary>
        public string Address { get; set; }
        /// <summary>
        /// 用于搜索具有特定星级的酒店
        /// </summary>
        public string Award { get; set; }
        /// <summary>
        /// 酒店电话号码
        /// </summary>
        public string ContactNumbers { get; set; }
        /// <summary>
        /// 用于指定佣金计划（如果适用）
        /// 可以是“Y”或2个字母数字供应商特定代码
        /// </summary>
        public string CommissionProgram { get; set; }
        /// <summary>
        /// 用于根据便利设施搜索酒店
        /// </summary>
        public string HotelAmenity { get; set; }
        /// <summary>
        /// 酒店特性【obj】
        /// </summary>
        public string HotelFeature { get; set; }
        /// <summary>
        /// 酒店信息
        /// </summary>
        public List<HotelRefItem> HotelRef { get; set; }
        /// <summary>
        /// 用于指定酒店套餐类型（如果适用）
        /// </summary>
        public string Package { get; set; }
        /// <summary>
        /// 用于指定兴趣点
        /// </summary>
        public string PointOfInterest { get; set; }
        /// <summary>
        /// 用于根据酒店类型搜索酒店
        /// APTS
        /// LUXRY
        /// </summary>
        public string PropertyType { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string RefPoint { get; set; }
        /// <summary>
        /// 用于根据床上用品类型搜索酒店
        /// </summary>
        public string RoomAmenity { get; set; }
    }

    /// <summary>
    /// 检索范围
    /// </summary>
    public class HotelSearchCriteria
    {
        /// <summary>
        /// 检索范围
        /// </summary>
        public Criterion Criterion { get; set; }
        /// <summary>
        /// 用于指定要返回的结果数
        /// 默认值为12，最大值为100
        /// </summary>
        public string NumProperties { get; set; }
    }

    /// <summary>
    /// 时间范围
    /// </summary>
    public class TimeSpan
    {
        /// <summary>
        /// 结束时间
        /// </summary>
        public string End { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public string Start { get; set; }
    }

    public class AvailRequestSegment
    {
        /// <summary>
        /// 额外的检索信息
        /// </summary>
        public string AdditionalAvail { get; set; }
        /// <summary>
        /// 客户信息
        /// </summary>
        public Customer Customer { get; set; }
        /// <summary>
        /// 客人数量
        /// </summary>
        public GuestCounts GuestCounts { get; set; }
        /// <summary>
        /// 检索范围
        /// </summary>
        public HotelSearchCriteria HotelSearchCriteria { get; set; }
        /// <summary>
        /// POS
        /// </summary>
        public string POS { get; set; }
        /// <summary>
        /// 用于指定合同或协商价格代码
        /// </summary>
        public string RatePlanCandidates { get; set; }
        /// <summary>
        /// 时间范围
        /// </summary>
        public TimeSpan TimeSpan { get; set; }
    }
}
