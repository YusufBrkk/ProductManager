using ProductApp.Core.Entities;

namespace ProductApp.Application.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(string email, string password);
        Task<string?> LoginAsync(string email, string password);
    }
}