
using Microsoft.EntityFrameworkCore;
using Franquias.Api.Data;
using Franquias.Api.Models;

namespace Franquias.Api.Repositories
{
    public class CobrancaRepository
    {
        private readonly DBFranquias _contexto;

        public CobrancaRepository(DBFranquias contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Cobranca>> ListarTodosAsync()
        {
            return await _contexto.Cobrancas
                .Include(c => c.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .ToListAsync();
        }

        public async Task<Cobranca?> BuscarPorIdAsync(int id)
        {
            return await _contexto.Cobrancas
                .Include(c => c.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<List<Cobranca>> ListarPorUnidadeAsync(int unidadeId)
        {
            return await _contexto.Cobrancas
                .Include(c => c.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .Where(c => c.UnidadeFranqueadaId == unidadeId)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Cobranca cobranca)
        {
            _contexto.Cobrancas.Add(cobranca);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Cobranca cobranca)
        {
            _contexto.Cobrancas.Update(cobranca);
            await _contexto.SaveChangesAsync();
        }
    }
}