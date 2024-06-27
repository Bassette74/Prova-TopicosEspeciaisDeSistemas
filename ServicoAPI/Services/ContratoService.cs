using Microsoft.EntityFrameworkCore;
using ServicoAPI.Data;
using ServicoAPI.Interfaces;
using ServicoAPI.Models;

namespace ServicoAPI.Services
{
    public class ContratoService : IContratoService
    {
        private readonly ApiDbContext _context;

        public ContratoService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<Contrato> CreateContratoAsync(Contrato contrato)
        {
            _context.Contratos.Add(contrato);
            await _context.SaveChangesAsync();
            return contrato;
        }

        public async Task<List<Servico>> GetServicosByClienteIdAsync(int clienteId)
        {
            return await _context.Contratos
                .Where(c => c.ClienteId == clienteId)
                .Include(c => c.Servico)
                .Select(c => c.Servico)
                .ToListAsync();
        }
    }
}
