﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using OnlineShopping.Entities;
using OnlineShopping.Entities.Settings;

namespace OnlineShopping.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts(string sortOption);
    }

    public class ProductService : IProductService
    {
        private readonly IShopperHistoryService _shopperHistoryService;
        private readonly ConnectionSettings _connectionSettings;
        private readonly IHttpClientWrapper _httpClientWrapper;


        public ProductService(IShopperHistoryService shopperHistoryService, IOptions<ConnectionSettings> connectionSettings, IHttpClientWrapper httpClientWrapper)
        {
            _shopperHistoryService = shopperHistoryService;
            _httpClientWrapper = httpClientWrapper;
            _connectionSettings = connectionSettings.Value;
        }

        public async Task<List<Product>> GetProducts(string sortOption)
        {
            try
            {
                var products = await GetAllProducts();

                switch (sortOption.ToLower())
            {
                case "low":
                    return SortByPrice(products,"low");
                case "high":
                    return SortByPrice(products, "high");
                case "ascending":
                    return SortByName(products, "ascending");
                case "descending":
                    return SortByName(products, "descending");
                case "recommended":
                    return await GetRecommendedProducts(products);
                default:
                    throw new ArgumentException($"Unknown sort option {sortOption}");
            }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<List<Product>> GetRecommendedProducts(List<Product> allProducts)
        {
            var shoppingHistoryService = await _shopperHistoryService.GetShopperHistories();

            var products = shoppingHistoryService.SelectMany(h => h.Products);
            var soldProducts = products.GroupBy(p => p.Name)
                .Select(@group => new Product
                {
                    Name = @group.Key,
                    Price = @group.First().Price,
                    Quantity = @group.Sum(g => g.Quantity)
                }).ToList();

            foreach (var product in allProducts)
            {
                product.Quantity = soldProducts.FirstOrDefault(p => p.Name.Equals(product.Name))?.Quantity ?? 0;
            }

            return allProducts.OrderByDescending(o => o.Quantity).ToList();
        }

        private List<Product> SortByPrice(List<Product> products, string sortOrder)
        {
            return sortOrder == "low"
                ? products.OrderBy(p => p.Price).ToList()
                : products.OrderByDescending(p => p.Price).ToList();
        }

        private List<Product> SortByName(List<Product> products, string sortOrder)
        {
            return sortOrder == "ascending"
                ? products.OrderBy(p => p.Name).ToList()
                : products.OrderByDescending(p => p.Name).ToList();
        }


        private async Task<List<Product>> GetAllProducts()
        {
            return await _httpClientWrapper.GetAsync<List<Product>>(
                $"{_connectionSettings.BaseUrl}/resource/products?token={_connectionSettings.Token}");
        }

    }
}
