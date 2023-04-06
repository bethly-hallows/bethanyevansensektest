using BethanyEvansENSEKTest.Models;
using BethanyEvansENSEKTest.Settings;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace BethanyEvansENSEKTest.Drivers
{
    public class ApiDriver
    {
        private readonly HttpClient _httpClient;

        public ApiDriver(EnsekApiSettings ensekApiSettings)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(ensekApiSettings.ApiEndpoint) };
        }

        public void AddAuthToken(string bearerToken)
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", bearerToken);
        }

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return _httpClient.GetAsync(url);
        }

        public Task<HttpResponseMessage> PostAsync(string url)
        {
            return _httpClient.PostAsync(url, null);
        }

        public Task<HttpResponseMessage> PutAsync(string url)
        {
            return _httpClient.PutAsync(url, null);
        }

        public Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T value)
        {
            return _httpClient.PostAsJsonAsync(url, value);
        }

        public Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return _httpClient.DeleteAsync(url);
        }

        public async Task<HttpResponseMessage> GetOrdersAsync()
        {
            const string url = "/ENSEK/orders";

            var httpResponseMessage = await GetAsync(url);

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> GetOrderAsync(string orderId)
        {
            var url = $"/ENSEK/orders/{orderId}";

            var httpResponseMessage = await GetAsync(url);

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> DeleteOrderAsync(string orderId)
        {
            var url = $"/ENSEK/orders/{orderId}";

            var httpResponseMessage = await DeleteAsync(url);

            return httpResponseMessage;
        }

        public async Task<HttpResponseMessage> PutBuyRequestAsync(int id, int quantity)
        {
            var url = $"/ENSEK/buy/{id}/{quantity}";

            var httpReponseMessage = await PutAsync(url);

            return httpReponseMessage;
        }

        public async Task<HttpResponseMessage> PostResetRequestAsync()
        {
            const string url = "/ENSEK/reset";

            var httpReponseMessage = await PostAsync(url);

            return httpReponseMessage;
        }

        public async Task<HttpResponseMessage> PostLoginRequestAsync(Login loginRequest)
        {
            const string url = "/ENSEK/login";

            var httpReponseMessage = await PostAsJsonAsync(url, loginRequest);

            return httpReponseMessage;
        }
    }
}
