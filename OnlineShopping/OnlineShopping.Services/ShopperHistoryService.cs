using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OnlineShopping.Entities;
using OnlineShopping.Entities.Settings;

namespace OnlineShopping.Services
{
    public interface IShopperHistoryService
    {
        Task<List<ShopperHistory>> GetShopperHistories();
    }

    public class ShopperHistoryService : IShopperHistoryService
    {
        private readonly ConnectionSettings _connectionSettings;
        private readonly IHttpClientWrapper _httpClientWrapper;


        public ShopperHistoryService(IOptions<ConnectionSettings> connectionSettings, IHttpClientWrapper httpClientWrapper)
        {
            _httpClientWrapper = httpClientWrapper;
            _connectionSettings = connectionSettings.Value;
        }

        public async Task<List<ShopperHistory>> GetShopperHistories()
        {
            return await _httpClientWrapper.GetAsync<List<ShopperHistory>>(
                $"{_connectionSettings.BaseUrl}/resource/shopperHistory?token={_connectionSettings.Token}");
        }
    }
}
