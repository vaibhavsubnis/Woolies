using BasicResponse.Models;

namespace BasicResponse.Services
{
    public interface IUserService
    {
        User GetUser();
    }

    public class UserService : IUserService
    {
        public User GetUser()
        {
            return new User { Name = "Vaibhav", Token = "f87e372c-e848-4481-9b81-0830d3dbbfa8" };
        }
    }
}
