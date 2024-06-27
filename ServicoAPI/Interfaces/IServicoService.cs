using ServicoAPI.Models;

namespace ServicoAPI.Interfaces
{
    public interface IServicoService
    {
        Task<Servico> CreateServicoAsync(Servico servico);
        Task<Servico> UpdateServicoAsync(int id, Servico servico);
        Task<Servico> GetServicoByIdAsync(int id);
    }
}
