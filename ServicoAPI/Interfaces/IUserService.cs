using ServicoAPI.Models;

namespace ServicoAPI.Interfaces
{
    public interface IUserService
    {
        Task RegisterUserAsync(User user);
        Task<string> AuthenticateAsync(string email, string password);
    }
}
