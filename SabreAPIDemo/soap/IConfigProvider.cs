using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfigProvider
    {
        /// <summary>
        /// Group
        /// </summary>
        string Group { get; }

        /// <summary>
        /// UserId
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// ClientSecret
        /// </summary>
        string ClientSecret { get; }

        /// <summary>
        /// Domain
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// 接口地址
        /// </summary>
        string Environment { get; }
    }
}
