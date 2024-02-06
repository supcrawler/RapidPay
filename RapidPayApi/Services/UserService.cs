using RapidPay.Core;
using RapidPayApi.Data.Models;
using RapidPayApi.Services.Interfaces;

namespace RapidPayApi.Services
{
    public class UserService : IUserService
    {
        private readonly List<User> _users = new() {
            new User { Id = 1, Username = Constants.AUTH_USERNAME, Password = Constants.AUTH_PASSWORD },
        };

        public async Task<User?> Authenticate(string username, string password)
        {
            var user = await Task.Run(
                () => _users.SingleOrDefault(
                    x => x.Username == username && x.Password == password
                )
            );

            return user != null ? new User { Id = user.Id, Email = user.Email, Username = user.Username } : null;
        }
    }
}