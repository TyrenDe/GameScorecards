using GameScorecardsModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace GameScorecardsClient.Services
{
    public abstract class RestService
    {
        public HttpClient HttpClient { get; private set; }

        public RestService(HttpClient client)
        {
            HttpClient = client;
        }

        protected async Task<RestResponse<TResponse, ErrorResponse>> GetAsync<TResponse>(string uri)
        {
            var response = await HttpClient.GetAsync(uri);
            return await HandleResponseAsync<TResponse>(response);
        }

        protected async Task<RestResponse<TResponse, ErrorResponse>> PostAsync<TRequest, TResponse>(string uri, TRequest request)
        {
            var content = JsonConvert.SerializeObject(request);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");

            var response = await HttpClient.PostAsync(uri, bodyContent);
            return await HandleResponseAsync<TResponse>(response);
        }

        // TODO: Add PATCH, DELETE, PUT, etc

        private static async Task<RestResponse<TResponse, ErrorResponse>> HandleResponseAsync<TResponse>(HttpResponseMessage response)
        {
            var result = new RestResponse<TResponse, ErrorResponse>
            {
                StatusCode = response.StatusCode,
            };

            var contentTemp = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                result.Response = JsonConvert.DeserializeObject<TResponse>(contentTemp);
            }
            else
            {
                result.Error = JsonConvert.DeserializeObject<ErrorResponse>(contentTemp);
            }

            return result;
        }
    }
}
