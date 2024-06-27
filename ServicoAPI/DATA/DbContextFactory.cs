using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ServicoAPI.Data;
using ServicoAPI.Interfaces;

namespace ServicoAPI.Services
{
    public class DbContextFactory : IDbContextFactory
    {
        private readonly IConfiguration _configuration;

        public DbContextFactory(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ApiDbContext CreateDbContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>();
            var connectionString = _configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new ApiDbContext(optionsBuilder.Options);
        }
    }
}
