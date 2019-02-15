using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo.model.soap
{
    /// <summary>
    /// 步骤6:结束乘客姓名记录的事务
    /// 用于完成和存储对乘客姓名记录 (pnr) 所做的更改
    /// </summary>
    public class EndTransactionRQ
    {
        /// <summary>
        /// 用于结束事务并完成记录
        /// </summary>
        public EndTransaction EndTransaction { get; set; }
        /// <summary>
        /// 【obj】
        /// </summary>
        public Source Source { get; set; }
        /// <summary>
        /// 响应数
        /// </summary>
        public string NumResponses { get; set; }
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

    public class EndTransaction
    {
        /// <summary>
        /// 用于在结束交易时向特定记录中包含的任何电子邮件地址发送电子邮件通知。
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// 确认
        /// </summary>
        public string Ind { get; set; }
    }

    public class Source
    {
        /// <summary>
        /// 用于接收记录
        /// </summary>
        public string ReceivedFrom { get; set; }
    }
}
