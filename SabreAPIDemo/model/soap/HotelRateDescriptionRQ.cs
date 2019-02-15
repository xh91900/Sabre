using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo.model.soap.HotelRateDescriptionRQ
{
    /// <summary>
    /// 步骤3:检索酒店规则和策略
    /// 提供了所有适用的规则、政策和限制, 以控制特定酒店房价的使用
    /// 包括所有费用的明细, 包括基本费率、住宿期间的变化、详细的税收以及总体估计成本
    /// </summary>
    public class HotelRateDescriptionRQ
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
    }

    /// <summary>
    /// 检索范围
    /// </summary>
    public class Criterion
    {
        /// <summary>
        /// 酒店信息
        /// </summary>
        public HotelRef HotelRef { get; set; }
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
    }

    /// <summary>
    /// 用于指定合同或协商价格代码
    /// </summary>
    public class RatePlanCandidate
    {
        /// <summary>
        /// 用于指定货币代码。 
        /// </summary>
        public string CurrencyCode { get; set; }
        /// <summary>
        /// 用于指定DCA产品代码
        /// </summary>
        public string DCA_ProductCode { get; set; }
        /// <summary>
        /// 用于指示系统TP解码非DCA属性的所有速率
        /// </summary>
        public string DecodeAllSpecified { get; set; }
        /// <summary>
        /// 用于指定费率代码
        /// </summary>
        public string RateCode { get; set; }
        /// <summary>
        /// 用于根据上一个hotelpropertiesDescriptionLLSRQ响应中的速率位置查看速率信息
        /// </summary>
        public string RPH { get; set; }
    }

    /// <summary>
    /// 用于指定合同或协商价格代码
    /// </summary>
    public class RatePlanCandidates
    {
        /// <summary>
        /// 用于指定合同或协商价格代码
        /// </summary>
        public RatePlanCandidate RatePlanCandidate { get; set; }
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
        public RatePlanCandidates RatePlanCandidates { get; set; }
        /// <summary>
        /// 时间范围
        /// </summary>
        public TimeSpan TimeSpan { get; set; }
    }
}
