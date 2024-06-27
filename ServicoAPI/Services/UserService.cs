using Microsoft.EntityFrameworkCore;
using ServicoAPI.Data;
using ServicoAPI.Helpers;
using ServicoAPI.Interfaces;
using ServicoAPI.Models;
using BCrypt.Net;

namespace ServicoAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApiDbContext _context;
        private readonly JwtHelper _jwtHelper;

        public UserService(ApiDbContext context, JwtHelper jwtHelper)
        {
            _context = context;
            _jwtHelper = jwtHelper;
        }

        public async Task RegisterUserAsync(User user)
        {
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<string> AuthenticateAsync(string email, string password)
        {
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Email == email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return null;
            }

            return _jwtHelper.GenerateToken(user.Id);
        }
    }
}
