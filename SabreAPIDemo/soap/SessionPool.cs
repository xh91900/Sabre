using SessionCreateRQApi;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SabreAPIDemo
{
    /// <summary>
    /// 会话池
    /// </summary>
    public class SessionPool
    {
        /// <summary>
        /// 会话标识符和使用中会话之间的映射
        /// </summary>
        private readonly ConcurrentDictionary<string, Security> busy = new ConcurrentDictionary<string, Security>();

        /// <summary>
        /// 可用会话队列.
        /// </summary>
        private readonly ConcurrentQueue<Security> availableQueue = new ConcurrentQueue<Security>();

        /// <summary>
        /// 回话数量.
        /// </summary>
        private readonly int count;

        /// <summary>
        /// 信号量.
        /// </summary>
        private readonly SemaphoreSlim semaphore;

        /// <summary>
        /// SOAP身份验证服务.
        /// </summary>
        private readonly SoapAuth soapAuth;

        /// <summary>
        /// 构造.
        /// </summary>
        /// <param name="soapAuth">SOAP身份验证服务.</param>
        public SessionPool(SoapAuth soapAuth, int count)
        {
            this.soapAuth = soapAuth;
            this.count = count;
            this.semaphore = new SemaphoreSlim(0, count);
        }

        /// <summary>
        /// 填充会话池，创建新会话.
        /// </summary>
        /// <returns>
        /// </returns>
        public void Populate()
        {
            int successes = 0;
            for (int i = 0; i < this.count; ++i)
            {
                try
                {
                    string conversationId = "AuthConversation_" + i;
                    // 日志
                    if (this.CreateSession(conversationId))
                    {
                        successes += 1;
                    }
                }
                catch (Exception ex)
                {
                    // 日志
                }
            }
        }

        /// <summary>
        /// 从会话池获取安全令牌.
        /// </summary>
        /// <param name="conversationId">会话标识符.</param>
        /// <returns>
        /// </returns>
        public Security TakeSessionAsync(string conversationId)
        {
            if (!this.busy.ContainsKey(conversationId))
            {
                // 日志
                //this.semaphore.Wait();
                // 日志
                try
                {
                    Security security;
                    bool succeeded = this.availableQueue.TryDequeue(out security);
                    if (succeeded)
                    {
                        this.busy[conversationId] = security;
                    }
                    else
                    {
                        // 日志
                        this.semaphore.Release();
                    }
                }
                catch (Exception)
                {
                    this.semaphore.Release();
                }
            }

            return this.busy[conversationId];
        }

        /// <summary>
        /// 释放会话.
        /// </summary>
        /// <param name="conversationId">会话标识符.</param>
        public void ReleaseSession(string conversationId)
        {
            Security security;
            bool removed = this.busy.TryRemove(conversationId, out security);
            if (removed)
            {
                // 日志
                this.availableQueue.Enqueue(security);
                this.semaphore.Release();
            }
            else
            {
                // 日志
                throw new KeyNotFoundException("Can't find session for this ConversationID.");
            }
        }

        /// <summary>
        /// 异步刷新会话池，更新会话.
        /// </summary>
        /// <returns>
        /// </returns>
        //public async Task RefreshAsync()
        //{
        //    bool canContinue = true;
        //    List<Security> refreshed = new List<Security>();
        //    List<Security> failed = new List<Security>();
        //    int i = 0;
        //    while (canContinue && this.semaphore.CurrentCount > 0)
        //    {
        //        await this.semaphore.WaitAsync();
        //        Security security = null;
        //        try
        //        {
        //            canContinue = this.availableQueue.TryDequeue(out security);
        //            if (canContinue)
        //            {
        //                var result = await this.soapAuth.TryRefreshAsync(security, "RefreshConversation_" + i);
        //                i += 1;
        //                if (result.IsOk)
        //                {
        //                    // 日志
        //                    refreshed.Add(security);
        //                }
        //                else
        //                {
        //                    // 日志
        //                    failed.Add(security);
        //                }
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            // 日志
        //            if (security == null)
        //            {
        //                failed.Add(security);
        //            }
        //        }
        //    }

        //    refreshed.ForEach(this.availableQueue.Enqueue);
        //    this.semaphore.Release(refreshed.Count);

        //    foreach (int j in failed.Select((o, j) => j))
        //    {
        //        if (await this.CreateSessionAsync("AuthConversation_" + j))
        //        {
        //            this.semaphore.Release();
        //        }
        //    }
        //}

        /// <summary>
        /// 创建会话并将其添加到可用队列.
        /// </summary>
        /// <param name="conversationId">会话标识符.</param>
        private bool CreateSession(string conversationId)
        {
            var authResult = this.soapAuth.CreateSession(conversationId);
            if (authResult.IsOk)
            {
                // 日志
                this.availableQueue.Enqueue(authResult.Security);
                return true;
            }
            else if (authResult.Exception != null)
            {
                // 日志
            }
            else
            {
                // 日志
            }

            return false;
        }
    }
}
