using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SabreAPIDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class RestAuthorizationManager
    {
        /// <summary>
        /// 
        /// </summary>
        private const string AuthorizationEndpoint = "/v2/auth/token";

        /// <summary>
        /// 
        /// </summary>
        private const string FormatVersion = "V1";

        /// <summary>
        /// 
        /// </summary>
        private readonly IConfigProvider config;

        /// <summary>
        /// 
        /// </summary>
        private TokenHolder tokenHolder = TokenHolder.Empty();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public RestAuthorizationManager(IConfigProvider config)
        {
            this.config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="credentials"></param>
        /// <returns></returns>
        public async Task<HttpResponse<AuthTokenRS>> AuthorizeAsync(string credentials)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                    client.BaseAddress = new Uri(this.config.Environment);
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
                    client.DefaultRequestHeaders.Add("grant_type", "client_credentials");
                    client.DefaultRequestHeaders.Add("Response-Type", "application/x-www-form-urlencoded");

                    var args = new Dictionary<string, string>();
                    //args.Add("grant_type", "client_credentials");
                    var content = new FormUrlEncodedContent(args);
                    client.PostAsync(AuthorizationEndpoint, content).GetAwaiter().OnCompleted(() => { Console.WriteLine("ready"); });
                    var response = await client.PostAsync(AuthorizationEndpoint, content);
                    string requestUri = response.RequestMessage.RequestUri.ToString();
                    if (response.IsSuccessStatusCode)
                    {
                        AuthTokenRS value = await response.Content.ReadAsAsync<AuthTokenRS>();
                        return HttpResponse<AuthTokenRS>.Success(response.StatusCode, value, requestUri);
                    }
                    else
                    {
                        return HttpResponse<AuthTokenRS>.Fail(response.StatusCode, await response.Content.ReadAsStringAsync(), requestUri);
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string CreateCredentialsString(string userId, string group, string secret, string domain = "AA", string formatVersion = "V1")
        {
            string clientId = string.Format("{0}:{1}:{2}:{3}", formatVersion, userId, group, domain);
            return Base64Encode(Base64Encode(clientId) + ":" + Base64Encode(secret));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="forceRefresh">true:更新token</param>
        /// <returns></returns>
        public async Task<TokenHolder> GetAuthorizationTokenAsync(bool forceRefresh = false)
        {
            if (forceRefresh || this.tokenHolder == null || this.tokenHolder.Token == null || this.tokenHolder.ExpirationDate <= DateTime.Now)
            {
                string userId = this.config.UserId;
                string group = this.config.Group;
                string secret = this.config.ClientSecret;
                string domain = this.config.Domain;
                string formatVersion = FormatVersion;
                string clientId = this.CreateCredentialsString(userId, group, secret, domain, formatVersion);
                var response = await this.AuthorizeAsync(clientId);
                if (response.IsSuccess)
                {
                    var value = response.Value;
                    this.tokenHolder = TokenHolder.Valid(value.access_token, value.expires_in);
                }
                else
                {
                    this.tokenHolder = TokenHolder.Invalid(response.StatusCode, response.Message);
                }
            }

            return this.tokenHolder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        private static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class AuthTokenRS
    {
        public string access_token { get; set; }

        public string token_type { get; set; }

        public int expires_in { get; set; }
    }
}
