using ServicoAPI.Data;

namespace ServicoAPI.Interfaces
{
    public interface IDbContextFactory
    {
        ApiDbContext CreateDbContext();
    }
}
