using ServicoAPI.Models;

namespace ServicoAPI.Interfaces
{
    public interface IContratoService
    {
        Task<Contrato> CreateContratoAsync(Contrato contrato);
        Task<List<Servico>> GetServicosByClienteIdAsync(int clienteId);
    }
}
