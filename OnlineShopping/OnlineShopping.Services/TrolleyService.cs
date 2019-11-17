using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OnlineShopping.Entities;
using OnlineShopping.Entities.Settings;

namespace OnlineShopping.Services
{
    public interface ITrolleyService
    {
        Task<decimal> GetLowestTrolleyTotal(TrolleyTotalRequest trolleyRequest);
    }

    public class TrolleyService : ITrolleyService
    {
        private readonly ConnectionSettings _connectionSettings;


        public TrolleyService(IOptions<ConnectionSettings> connectionSettings)
        {
            _connectionSettings = connectionSettings.Value;
        }

        public async Task<decimal> GetLowestTrolleyTotal(TrolleyTotalRequest trolleyRequest)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_connectionSettings.BaseUrl);

                var request = new HttpRequestMessage(HttpMethod.Post, $"/api/resource/trolleyCalculator?token={_connectionSettings.Token}")
                {
                    Content = new StringContent(JsonConvert.SerializeObject(trolleyRequest), Encoding.UTF8, "application/json")
                };

                var response = await client.SendAsync(request);

                return JsonConvert.DeserializeObject<decimal>(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
