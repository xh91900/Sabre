using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo.model.soap.HotelPropertyDescriptionRQ
{
    /// <summary>
    /// 步骤2:检索酒店价格
    /// </summary>
    public class HotelPropertyDescriptionRQ
    {
        /// <summary>
        /// 请求段
        /// </summary>
        public AvailRequestSegment AvailRequestSegment { get; set; }
        /// <summary>
        /// 用于指定是否在响应消息中返回本机主机命令
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
    /// 客人数量
    /// </summary>
    public class GuestCounts
    {
        /// <summary>
        /// 用于指定房间居住人数-最多9人。
        /// </summary>
        public string Count { get; set; }
    }

    /// <summary>
    /// 酒店信息
    /// </summary>
    public class HotelRef
    {
        /// <summary>
        /// 酒店代码
        /// </summary>
        public string HotelCode { get; set; }
        /// <summary>
        /// 用于覆盖销售点位置的默认搜索度量
        /// </summary>
        public string UnitOfMeasureSpecified { get; set; }
    }

    /// <summary>
    /// 检索条件
    /// </summary>
    public class Criterion
    {
        /// <summary>
        /// 酒店信息
        /// </summary>
        public HotelRef HotelRef { get; set; }
    }

    /// <summary>
    /// 检索条件
    /// </summary>
    public class HotelSearchCriteria
    {
        /// <summary>
        /// 检索条件
        /// </summary>
        public Criterion Criterion { get; set; }
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

    /// <summary>
    /// 请求段
    /// </summary>
    public class AvailRequestSegment
    {
        /// <summary>
        /// 客户信息【obj】
        /// </summary>
        public string Customer { get; set; }
        /// <summary>
        /// 客人数量
        /// </summary>
        public GuestCounts GuestCounts { get; set; }
        /// <summary>
        /// 检索条件
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
