using Microsoft.Extensions.Options;
using NUnit.Framework;
using OnlineShopping.Entities.Settings;
using OnlineShopping.Services;

namespace OnlineShopping.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService _userService;

        [Test]
        public void GetUserTest_ShouldGetUserSuccessfully()
        {
            IOptions<ConnectionSettings> connectionSettings = Options.Create(new ConnectionSettings{Name = "Test", Token = "000-111-222-333"});
            _userService = new UserService(connectionSettings);

            var result = _userService.GetUser();

            Assert.IsNotNull(result);
            Assert.AreEqual(connectionSettings.Value.Name, result.Name);
            Assert.AreEqual(connectionSettings.Value.Token, result.Token);
        }

        // Create more test scenarios including negative scenarios.
    }
}