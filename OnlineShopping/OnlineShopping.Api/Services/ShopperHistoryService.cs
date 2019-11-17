using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OnlineShopping.Api.Models;
using OnlineShopping.Api.Settings;

namespace OnlineShopping.Api.Services
{
    public interface IShopperHistoryService
    {
        Task<List<ShopperHistory>> GetShopperHistories();
    }

    public class ShopperHistoryService : IShopperHistoryService
    {
        private readonly ConnectionSettings _connectionSettings;

        public ShopperHistoryService(IOptions<ConnectionSettings> connectionSettings)
        {
            _connectionSettings = connectionSettings.Value;
        }

        public async Task<List<ShopperHistory>> GetShopperHistories()
        {
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(new Uri($"{_connectionSettings.BaseUrl}/resource/shopperHistory?token={_connectionSettings.Token}"));

                return JsonConvert.DeserializeObject<List<ShopperHistory>>(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}
