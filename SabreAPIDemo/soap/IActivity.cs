using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo
{
    /// <summary>
    /// 工作流活动
    /// </summary>
    public interface IActivity
    {
        /// <summary>
        /// 运行的活动
        /// </summary>
        /// <param name="sharedContext"></param>
        /// <returns></returns>
        IActivity Run(SharedContext sharedContext);
    }
}
