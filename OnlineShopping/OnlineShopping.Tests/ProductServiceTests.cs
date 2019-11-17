using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using OnlineShopping.Entities;
using OnlineShopping.Entities.Settings;
using OnlineShopping.Services;

namespace OnlineShopping.Tests
{
    [TestFixture()]
    public class ProductServiceTests
    {
        private IShopperHistoryService _shopperHistoryService = Substitute.For<IShopperHistoryService>();
        private IHttpClientWrapper _httpClientWrapper = Substitute.For<IHttpClientWrapper>();
        IOptions<ConnectionSettings> _connectionSettings = Options.Create(new ConnectionSettings());

        private List<Product> _products = new List<Product>
        {
            new Product {Name = "Test1", Quantity = 1, Price = 10},
            new Product {Name = "Test2", Quantity = 2, Price = 5},
            new Product {Name = "Test3", Quantity = 3, Price = 30}
        };

        [Test]
        public async Task GetProducts_ShouldReturnPriceInLowToHighOrder()
        {
            _httpClientWrapper.GetAsync<List<Product>>(Arg.Any<string>())
                .Returns(Task.FromResult(_products));

            var service = new ProductService(_shopperHistoryService, _connectionSettings, _httpClientWrapper);

            var result = await service.GetProducts("low");

            Assert.AreEqual(_products.OrderBy(p => p.Price), result);
        }

        [Test]
        public async Task GetProducts_ShouldReturnPriceInHighToLowOrder()
        {
            _httpClientWrapper.GetAsync<List<Product>>(Arg.Any<string>())
                .Returns(Task.FromResult(_products));

            var service = new ProductService(_shopperHistoryService, _connectionSettings, _httpClientWrapper);

            var result = await service.GetProducts("high");

            Assert.AreEqual(_products.OrderByDescending(p => p.Price), result);
        }

        [Test]
        public async Task GetProducts_ShouldReturnInNameAlphabeticalAscendingOrder()
        {
            _httpClientWrapper.GetAsync<List<Product>>(Arg.Any<string>())
                .Returns(Task.FromResult(_products));

            var service = new ProductService(_shopperHistoryService, _connectionSettings, _httpClientWrapper);

            var result = await service.GetProducts("ascending");

            Assert.AreEqual(_products.OrderBy(p => p.Name), result);
        }

        [Test]
        public async Task GetProducts_ShouldReturnInNameAlphabeticalDescendingOrder()
        {
            _httpClientWrapper.GetAsync<List<Product>>(Arg.Any<string>())
                .Returns(Task.FromResult(_products));

            var service = new ProductService(_shopperHistoryService, _connectionSettings, _httpClientWrapper);

            var result = await service.GetProducts("descending");

            Assert.AreEqual(_products.OrderByDescending(p => p.Name), result);
        }

        [Test]
        public async Task GetProducts_ShouldReturnInMostPopularFirstOrder()
        {
            _httpClientWrapper.GetAsync<List<Product>>(Arg.Any<string>())
                .Returns(Task.FromResult(_products));

            _shopperHistoryService.GetShopperHistories().Returns(Task.FromResult(new List<ShopperHistory>
            {
                new ShopperHistory{CustomerId = 123, Products = new List<Product> { new Product { Name = "Test1", Quantity = 10} }},
                new ShopperHistory{CustomerId = 125, Products = new List<Product> { new Product { Name = "Test2", Quantity = 12} }},
                new ShopperHistory{CustomerId = 124, Products = new List<Product> { new Product { Name = "Test3", Quantity = 9} }}
            }));

            var service = new ProductService(_shopperHistoryService, _connectionSettings, _httpClientWrapper);

            var result = await service.GetProducts("recommended");

            Assert.AreEqual("Test2", result[0].Name);
            Assert.AreEqual("Test1", result[1].Name);
            Assert.AreEqual("Test3", result[2].Name);
        }

        [Test]
        public async Task GetProducts_InvalidSortOrder_ShouldThrowException()
        {
            var service = new ProductService(_shopperHistoryService, _connectionSettings, _httpClientWrapper);

            Assert.CatchAsync<ArgumentException>(() => service.GetProducts("invalid"));

        }
    }
}
