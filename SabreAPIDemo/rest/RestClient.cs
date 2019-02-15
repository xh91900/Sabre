using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;
using System.Net.Http.Headers;

namespace SabreAPIDemo
{
    /// <summary>
    /// 
    /// </summary>
    public class RestClient
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IConfigProvider config;

        /// <summary>
        /// 
        /// </summary>
        public string authorizationToken;

        /// <summary>
        /// 
        /// </summary>
        private readonly RestAuthorizationManager restAuthorizationManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="restAuthorizationManager"></param>
        /// <param name="config"></param>
        public RestClient(RestAuthorizationManager restAuthorizationManager, IConfigProvider config)
        {
            this.config = config;
            this.restAuthorizationManager = restAuthorizationManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="requestUri"></param>
        /// <returns></returns>
        public async Task<HttpResponse<TResponse>> GetAsync<TResponse>(string requestUri)
        {
            return await this.CallAuthRetry<TResponse>(httpClient => httpClient.GetAsync(requestUri));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="path"></param>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        public async Task<HttpResponse<TResponse>> PostAsync<TRequest, TResponse>(string path, TRequest requestModel)
        {
            string json = JsonConvert.SerializeObject(requestModel);
            HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
            return await this.CallAuthRetry<TResponse>(httpClient => httpClient.PostAsync(path, content));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="method"></param>
        /// <param name="retryCount"></param>
        /// <param name="forceRefresh"></param>
        /// <returns></returns>
        private async Task<HttpResponse<TResponse>> CallAuthRetry<TResponse>(Func<HttpClient, Task<HttpResponseMessage>> method, int retryCount = 1, bool forceRefresh = false)
        {
            TokenHolder tokenHolder = await this.restAuthorizationManager.GetAuthorizationTokenAsync(forceRefresh);
            if (tokenHolder.IsValid)
            {
                var response = await this.Call<TResponse>(method, tokenHolder.Token);
                if (response.StatusCode == HttpStatusCode.Unauthorized && retryCount > 0)
                {
                    return await this.CallAuthRetry<TResponse>(method, retryCount - 1, true);
                }

                return response;
            }
            else
            {
                return HttpResponse<TResponse>.Fail(tokenHolder.ErrorStatusCode, tokenHolder.ErrorMessage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TResponse"></typeparam>
        /// <param name="method"></param>
        /// <param name="authorizationToken"></param>
        /// <returns></returns>
        private async Task<HttpResponse<TResponse>> Call<TResponse>(Func<HttpClient, Task<HttpResponseMessage>> method, string authorizationToken = null)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(this.config.Environment);
                if (authorizationToken != null)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);
                }

                var response = await method(client);
                string requestUri = response.RequestMessage.RequestUri.ToString();
                if (response.IsSuccessStatusCode)
                {
                    TResponse value = await response.Content.ReadAsAsync<TResponse>();
                    return HttpResponse<TResponse>.Success(response.StatusCode, value, requestUri);
                }
                else
                {
                    string message = await response.Content.ReadAsStringAsync();
                    return HttpResponse<TResponse>.Fail(response.StatusCode, message, requestUri);
                }
            }
        }
    }
}
