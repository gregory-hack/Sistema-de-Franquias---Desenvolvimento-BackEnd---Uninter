
using Microsoft.EntityFrameworkCore;
using Franquias.Api.Data;
using Franquias.Api.Models;

namespace Franquias.Api.Repositories
{
    public class UsuarioRepository
    {
        private readonly DBFranquias _contexto;

        public UsuarioRepository(DBFranquias contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Usuario>> ListarTodosAsync()
        {

            return await _contexto.Usuarios.Include(u => u.Perfil).ToListAsync();
        }
    
        public async Task<Usuario?> BuscarPorIdAsync(int id)
        {
            return await _contexto.Usuarios
            .Include(u => u.Perfil)
            .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Usuario?> BuscarPorEmailAsync(string email)
{
    return await _contexto.Usuarios
        .Include(u => u.Perfil)
        .FirstOrDefaultAsync(u => u.Email == email);
}

        public async Task AdicionarAsync(Usuario usuario)
        {
            _contexto.Usuarios.Add(usuario);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Usuario usuario)
        {
            _contexto.Usuarios.Update(usuario);
            await _contexto.SaveChangesAsync();
        }

        public async Task RemoverAsync(Usuario usuario)
        {
            _contexto.Usuarios.Remove(usuario);
            await _contexto.SaveChangesAsync();
        }
    }
}