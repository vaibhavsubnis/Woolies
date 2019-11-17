using NUnit.Framework;
using OnlineShopping.Api.Services;

namespace OnlineShopping.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserService _userService;

        [Test]
        public void GetUserTest_ShouldGetUserSuccessfully()
        {
            _userService = new UserService();

            var result = _userService.GetUser();

            Assert.IsNotNull(result);
            Assert.AreEqual("Vaibhav Subnis", result.Name);
            Assert.IsNotNull(result.Token);
        }

        // Create more test scenarios including negative scenarios.
    }
}