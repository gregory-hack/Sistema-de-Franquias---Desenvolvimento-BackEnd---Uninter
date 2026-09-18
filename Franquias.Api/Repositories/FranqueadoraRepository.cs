
using Microsoft.EntityFrameworkCore;
using Franquias.Api.Data;
using Franquias.Api.Models;

namespace Franquias.Api.Repositories
{
    public class FranqueadoraRepository
    {
        private readonly DBFranquias _contexto;

        public FranqueadoraRepository(DBFranquias contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Franqueadora>> ListarTodosAsync()
        {
            return await _contexto.Franqueadoras.ToListAsync();
        }

        public async Task<Franqueadora?> BuscarPorIdAsync(int id)
        {
            return await _contexto.Franqueadoras.FindAsync(id);
        }

        public async Task AdicionarAsync(Franqueadora franqueadora)
        {
            _contexto.Franqueadoras.Add(franqueadora);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Franqueadora franqueadora)
        {
            _contexto.Franqueadoras.Update(franqueadora);
            await _contexto.SaveChangesAsync();
        }

        public async Task RemoverAsync(Franqueadora franqueadora)
        {
            _contexto.Franqueadoras.Remove(franqueadora);
            await _contexto.SaveChangesAsync();
        }
    }
}