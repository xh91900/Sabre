using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo
{
    /// <summary>
    /// api工作流程
    /// </summary>
    public class Workflow
    {
        /// <summary>
        /// 字符序列
        /// </summary>
        private const string Alphabet = "abcdefghijklmnopqrstuvwyxzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        /// <summary>
        /// 用于生产随机字符串
        /// </summary>
        private static readonly Random Rand = new Random();

        /// <summary>
        /// 开始活动
        /// </summary>
        private readonly IActivity startActivity;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="startActivity"></param>
        public Workflow(IActivity startActivity)
        {
            this.startActivity = startActivity;
        }

        /// <summary>
        /// 运行整个工作流程
        /// 首先创建共享上下文并运行第一个活动
        /// 然后运行返回的活动等，直到上下文出错为止
        /// 最后一个活动返回null
        /// </summary>
        /// <returns></returns>
        public SharedContext Run()
        {
            // 日志
            IActivity activity = this.startActivity;
            string longRandom = new string(Enumerable.Range(0, 8).Select(i => Alphabet[Rand.Next(Alphabet.Length)]).ToArray());
            string conversationId = DateTime.Now.ToString("YYYYMMddhhmmss") + "-" + longRandom;
            SharedContext sharedContext = new SharedContext(conversationId);
            while (activity != null && !sharedContext.IsFaulty)
            {
                try
                {
                    activity = activity.Run(sharedContext);
                }
                catch (Exception ex)
                {
                    // 日志
                    sharedContext.IsFaulty = true;
                    throw;
                }
            }
            return sharedContext;
        }
    }
}
