using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OTA_HotelAvailServiceDemo;
using System.Collections.Concurrent;

namespace SabreSoapAPIDemo.soap
{
    public class SessionPool : ISessionPool
    {
        /// <summary>
        /// The mapping between conversation identifiers and busy sessions.
        /// </summary>
        private readonly ConcurrentDictionary<string, Security1> busy = new ConcurrentDictionary<string, Security1>();

        /// <summary>
        /// The queue of available sessions.
        /// </summary>
        private readonly ConcurrentQueue<Security1> availableQueue = new ConcurrentQueue<Security1>();

        public Task PopulateAsync()
        {
            throw new NotImplementedException();
        }

        public Task RefreshAsync()
        {
            throw new NotImplementedException();
        }

        public void ReleaseSession(string conversationId)
        {
            throw new NotImplementedException();
        }

        public Security1 TakeSessionAsync(string conversationId)
        {
            if (!this.busy.ContainsKey(conversationId))
            {
                try
                {
                    Security1 security;
                    bool succeeded = this.availableQueue.TryDequeue(out security);
                    if (succeeded)
                    {
                        this.busy[conversationId] = security;
                    }
                    else
                    {
                    }
                }
                catch (Exception)
                {
                }
            }

            return this.busy[conversationId];
        }
    }
}
