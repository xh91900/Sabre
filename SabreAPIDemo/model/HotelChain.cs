using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo.model
{
    /// <summary>
    /// 酒店连锁信息
    /// </summary>
    public class HotelChain
    {
        /// <summary>
        /// 
        /// </summary>
        public GetHotelChainInfoRS GetHotelChainInfoRS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public List<LinksItem> Links { get; set; }
    }

    public class SuccessItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string timeStamp { get; set; }
    }

    /// <summary>
    /// 应用返回
    /// </summary>
    public class ApplicationResults
    {
        /// <summary>
        /// 成功信息
        /// </summary>
        public List<SuccessItem> Success { get; set; }
    }

    /// <summary>
    /// 连锁信息
    /// </summary>
    public class ChainItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// 连锁信息集合
    /// </summary>
    public class Chains
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ChainItem> Chain { get; set; }
    }

    /// <summary>
    /// 营销信息
    /// </summary>
    public class MarketerItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Chains Chains { get; set; }
    }

    /// <summary>
    /// 营销信息集合
    /// </summary>
    public class Marketers
    {
        /// <summary>
        /// 
        /// </summary>
        public List<MarketerItem> Marketer { get; set; }
    }

    /// <summary>
    /// 获取酒店连锁信息
    /// </summary>
    public class GetHotelChainInfoRS
    {
        /// <summary>
        /// 
        /// </summary>
        public ApplicationResults ApplicationResults { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Marketers Marketers { get; set; }
    }

    /// <summary>
    /// 链接
    /// </summary>
    public class LinksItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string rel { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string href { get; set; }
    }
}
