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

        public Task<HttpResponseMessage> GetAsync(string url)
        {
            return _httpClient.GetAsync(url);
        }

        public Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T value)
        {
            return _httpClient.PostAsJsonAsync(url, value);
        }

        public Task<HttpResponseMessage> PutAsJsonAsync<T>(string url, T value)
        {
            return _httpClient.PutAsJsonAsync(url, value);
        }

        public Task<HttpResponseMessage> DeleteAsync(string url)
        {
            return _httpClient.DeleteAsync(url);
        }

        public void PrepareOrderRequest()
        {

        }

        public void PutOrderRequest(int orderId)
        {
            var url = $"/ENSEK/orders/{orderId}";
        }

        public async Task<HttpResponseMessage> PostResetRequestAsync()
        {
            const string url = "/ENSEK/reset";

            var httpReponseMessage = await GetAsync(url);

            return httpReponseMessage;
        }
    }
}
