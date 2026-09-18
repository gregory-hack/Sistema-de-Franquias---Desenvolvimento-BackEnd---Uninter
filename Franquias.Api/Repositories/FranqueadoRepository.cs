
using Microsoft.EntityFrameworkCore;
using Franquias.Api.Data;
using Franquias.Api.Models;

namespace Franquias.Api.Repositories
{
    public class FranqueadoRepository
    {
        private readonly DBFranquias _contexto;

        public FranqueadoRepository(DBFranquias contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Franqueado>> ListarTodosAsync()
        {
            return await _contexto.Franqueados
                .Include(f => f.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .ToListAsync();
        }

        public async Task<Franqueado?> BuscarPorIdAsync(int id)
        {
            return await _contexto.Franqueados
                .Include(f => f.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task AdicionarAsync(Franqueado franqueado)
        {
            _contexto.Franqueados.Add(franqueado);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Franqueado franqueado)
        {
            _contexto.Franqueados.Update(franqueado);
            await _contexto.SaveChangesAsync();
        }

        public async Task RemoverAsync(Franqueado franqueado)
        {
            _contexto.Franqueados.Remove(franqueado);
            await _contexto.SaveChangesAsync();
        }
    }
}