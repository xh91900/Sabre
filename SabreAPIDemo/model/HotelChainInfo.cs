using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo.model.soap
{
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

    public class Chains
    {
        /// <summary>
        /// 
        /// </summary>
        public List<ChainItem> Chain { get; set; }
    }

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

    public class Marketers
    {
        /// <summary>
        /// 
        /// </summary>
        public List<MarketerItem> Marketer { get; set; }
    }

    public class GetHotelChainInfoRS
    {
        /// <summary>
        /// 
        /// </summary>
        public Marketers Marketers { get; set; }
    }

    public class Root
    {
        /// <summary>
        /// 
        /// </summary>
        public GetHotelChainInfoRS GetHotelChainInfoRS { get; set; }
    }
}
