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
    public class TokenHolder
    {
        /// <summary>
        /// 
        /// </summary>
        public string Token { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ExpirationDate { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsValid { get; private set; }
        
        /// <summary>
        /// 
        /// </summary>
        public HttpStatusCode ErrorStatusCode { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string ErrorMessage { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static TokenHolder Empty()
        {
            return new TokenHolder
            {
                ExpirationDate = DateTime.MinValue,
                IsValid = false,
                Token = null
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="expirationSeconds"></param>
        /// <returns></returns>
        public static TokenHolder Valid(string token, int expirationSeconds)
        {
            return new TokenHolder
            {
                ExpirationDate = DateTime.Now.AddSeconds(expirationSeconds),
                IsValid = true,
                Token = token
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpStatusCode"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static TokenHolder Invalid(HttpStatusCode httpStatusCode, string message)
        {
            var tokenHolder = Empty();
            tokenHolder.ErrorStatusCode = httpStatusCode;
            tokenHolder.ErrorMessage = message;
            return tokenHolder;
        }
    }
}
