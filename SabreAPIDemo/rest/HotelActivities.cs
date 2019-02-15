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
    public class HotelActivities : IActivity
    {
        /// <summary>
        /// 
        /// </summary>
        private const string JsonSharedContextKey = "InstaFlightsJson";

        /// <summary>
        /// 
        /// </summary>
        private const string RequestUriSharedContextKey = "InstaFlightsRequestUri";

        /// <summary>
        /// 
        /// </summary>
        private const string SharedContextKey = "InstaFlightsSearchActivity";

        /// <summary>
        /// 
        /// </summary>
        private const string Endpoint = "/v1/shop/flights";

        /// <summary>
        /// 
        /// </summary>
        //private readonly IInstaFlightsData data;

        /// <summary>
        /// restClient
        /// </summary>
        private readonly RestClient restClient;

        /// <summary>
        /// 
        /// </summary>
        //public HotelActivities(RestClient restClient, IInstaFlightsData data = null)
        //{
        //    this.data = data;
        //    this.restClient = restClient;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sharedContext"></param>
        /// <returns></returns>
        public Task<IActivity> RunAsync(SharedContext sharedContext)
        {
            throw new NotImplementedException();
        }

        IActivity IActivity.Run(SharedContext sharedContext)
        {
            throw new NotImplementedException();
        }
    }
}
