using Microsoft.EntityFrameworkCore;
using ServicoAPI.Data;
using ServicoAPI.Interfaces;
using ServicoAPI.Models;

namespace ServicoAPI.Services
{
    public class ServicoService : IServicoService
    {
        private readonly ApiDbContext _context;

        public ServicoService(ApiDbContext context)
        {
            _context = context;
        }

        public async Task<Servico> CreateServicoAsync(Servico servico)
        {
            _context.Servicos.Add(servico);
            await _context.SaveChangesAsync();
            return servico;
        }

        public async Task<Servico> UpdateServicoAsync(int id, Servico servico)
        {
            var existingServico = await _context.Servicos.FindAsync(id);
            if (existingServico == null) return null;

            existingServico.Nome = servico.Nome;
            existingServico.Preco = servico.Preco;
            existingServico.Status = servico.Status;

            _context.Servicos.Update(existingServico);
            await _context.SaveChangesAsync();
            return existingServico;
        }

        public async Task<Servico> GetServicoByIdAsync(int id)
        {
            return await _context.Servicos.FindAsync(id);
        }
    }
}
