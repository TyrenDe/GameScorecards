using GameScorecardsModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GameScorecardsClient.Services
{
    public abstract class RestService
    {
        public HttpClient HttpClient { get; private set; }

        public RestService(HttpClient client)
        {
            HttpClient = client;
        }

        protected async Task<RestResponse<TResponse>> GetAsync<TResponse>(string uri)
        {
            return await GetAsync<TResponse>(uri, null);
        }

        protected async Task<RestResponse<TResponse>> GetAsync<TResponse>(string uri, string requestId)
        {
            if (!string.IsNullOrEmpty(requestId))
            {
                uri = uri.AddParameter("requestId", requestId);
            }

            var response = await HttpClient.GetAsync(uri);
            var contentTemp = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RestResponse<TResponse>>(contentTemp);
        }

        protected async Task<RestResponse<TResponse>> PostAsync<TRequest, TResponse>(string uri, RestRequest<TRequest> request)
            where TRequest : class
        {
            var content = JsonConvert.SerializeObject(request);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await HttpClient.PostAsync(uri, bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<RestResponse<TResponse>>(contentTemp);
        }
    }
}
