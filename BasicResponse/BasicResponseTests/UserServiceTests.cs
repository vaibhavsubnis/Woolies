using BasicResponse.Services;
using NUnit.Framework;

namespace BasicResponseTests
{
    public class UserServiceTests
    {
        private IUserService _userService;

        [Test]
        public void GetUserTest_ShouldGetUserSuccessfully()
        {
            _userService = new UserService();

            var result = _userService.GetUser();

            Assert.IsNotNull(result);
            Assert.AreEqual("Vaibhav", result.Name);
            Assert.IsNotNull(result.Token);
        }

        // Create more test scenarios including negative scenarios.
    }
}