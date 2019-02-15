using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo
{
    /// <summary>
    /// 用于在活动之间传递信息并返回工作流中最后一个活动的结果的共享上下文
    /// </summary>
    public class SharedContext
    {
        /// <summary>
        /// 结果字典
        /// </summary>
        private readonly Dictionary<string, object> results = new Dictionary<string, object>();

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="conversationId">.</param>
        public SharedContext(string conversationId)
        {
            this.ConversationId = conversationId;
        }

        /// <summary>
        /// 获取会话标识符
        /// </summary>
        public string ConversationId { get; private set; }

        /// <summary>
        /// 标识工作流中是否出错
        /// </summary>
        public bool IsFaulty { get; set; }

        /// <summary>
        /// 将结果添加到字典
        /// </summary>
        /// <param name="key"></param>
        /// <param name="result"></param>
        public void AddResult(string key, object result)
        {
            this.results.Add(key, result);
        }

        /// <summary>
        /// 根据key获取结果
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetResult(string key)
        {
            object value;
            if (this.results.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 根据key获取结果并将其强制转换为指定的类型
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public TResult GetResult<TResult>(string key)
            where TResult : class
        {
            object value;
            if (this.results.TryGetValue(key, out value))
            {
                return value as TResult;
            }
            else
            {
                return null;
            }
        }
    }
}
