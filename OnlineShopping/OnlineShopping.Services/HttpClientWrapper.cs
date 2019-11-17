using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OnlineShopping.Entities;

namespace OnlineShopping.Services
{
    public interface IHttpClientWrapper
    {
        Task<T> GetAsync<T>(string uri);
        Task<TResponse> PostAsync<TResponse, TRequest>(TRequest request, string baseUrl, string requestUrl);
    }

    public class HttpClientWrapper : IHttpClientWrapper
    {
        public async Task<T> GetAsync<T>(string uri)
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(new Uri(uri));

                return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
            }

        }

        public async Task<TResponse> PostAsync<TResponse, TRequest>(TRequest request, string baseUrl, string requestUrl)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(baseUrl);

                var httpRequest = new HttpRequestMessage(HttpMethod.Post, requestUrl)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(httpRequest);

                return JsonConvert.DeserializeObject<TResponse>(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
