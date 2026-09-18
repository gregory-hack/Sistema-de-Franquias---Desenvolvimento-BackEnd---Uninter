
using Microsoft.EntityFrameworkCore;
using Franquias.Api.Data;
using Franquias.Api.Models;

namespace Franquias.Api.Repositories
{
    public class PerfilRepository
    {
        private readonly DBFranquias _contexto;

        public PerfilRepository(DBFranquias contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Perfil>> ListarTodosAsync()
        {
            return await _contexto.Perfis.ToListAsync();
        }

        public async Task<Perfil?> BuscarPorIdAsync(int id)
        {
            return await _contexto.Perfis.FindAsync(id);
        }

        public async Task AdicionarAsync(Perfil perfil)
        {
            _contexto.Perfis.Add(perfil);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Perfil perfil)
        {
            _contexto.Perfis.Update(perfil);
            await _contexto.SaveChangesAsync();
        }

        public async Task RemoverAsync(Perfil perfil)
        {
            _contexto.Perfis.Remove(perfil);
            await _contexto.SaveChangesAsync();
        }
    }
}