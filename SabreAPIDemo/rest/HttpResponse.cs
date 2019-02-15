using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpResponse<TResponse>
    {
        /// <summary>
        /// 
        /// </summary>
        public TResponse Value { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string RequestUri { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="value"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public static HttpResponse<TResponse> Success(HttpStatusCode httpStatusCode, TResponse value, string requestUri = "")
        {
            return new HttpResponse<TResponse>
            {
                Value = value,
                IsSuccess = true,
                StatusCode = httpStatusCode,
                RequestUri = requestUri
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="message"></param>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public static HttpResponse<TResponse> Fail(HttpStatusCode httpStatusCode, string message, string requestUri = "")
        {
            return new HttpResponse<TResponse>
            {
                Message = message,
                StatusCode = httpStatusCode,
                RequestUri = requestUri,
            };
        }
    }
}
