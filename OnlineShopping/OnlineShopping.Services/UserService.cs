using Microsoft.Extensions.Options;
using OnlineShopping.Entities;
using OnlineShopping.Entities.Settings;

namespace OnlineShopping.Services
{
    public interface IUserService
    {
        User GetUser();
    }

    public class UserService : IUserService
    {
        private readonly ConnectionSettings _connectionSettings;

        public UserService(IOptions<ConnectionSettings> connectionSettings)
        {
            _connectionSettings = connectionSettings.Value;
        }

        public User GetUser()
        {
            return new User { Name = _connectionSettings.Name, Token = _connectionSettings.Token };
        }
    }
}
