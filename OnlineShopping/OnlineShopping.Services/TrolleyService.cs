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
        private readonly IHttpClientWrapper _httpClientWrapper;


        public TrolleyService(IOptions<ConnectionSettings> connectionSettings, IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
            _connectionSettings = connectionSettings.Value;
        }

        public async Task<decimal> GetLowestTrolleyTotal(TrolleyTotalRequest trolleyRequest)
        {
            return await _httpClientWrapper.PostAsync<decimal, TrolleyTotalRequest>(trolleyRequest,
                _connectionSettings.BaseUrl, $"/api/resource/trolleyCalculator?token={_connectionSettings.Token}");
        }
    }
}
