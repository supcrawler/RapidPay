using RapidPayApi.Data.Models;

namespace RapidPayApi.Services.Interfaces;

public interface IUserService
{
    Task<User?> Authenticate(string username, string password);
}