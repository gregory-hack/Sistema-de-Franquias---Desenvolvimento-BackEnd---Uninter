
using Microsoft.EntityFrameworkCore;
using Franquias.Api.Data;
using Franquias.Api.Models;

namespace Franquias.Api.Repositories
{
    public class CategoriaRepository
    {
        private readonly DBFranquias _contexto;

        public CategoriaRepository(DBFranquias contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Categoria>> ListarTodosAsync()
        {
            return await _contexto.Categorias.ToListAsync();
        }

        public async Task<Categoria?> BuscarPorIdAsync(int id)
        {
            return await _contexto.Categorias.FindAsync(id);
        }

        public async Task AdicionarAsync(Categoria categoria)
        {
            _contexto.Categorias.Add(categoria);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Categoria categoria)
        {
            _contexto.Categorias.Update(categoria);
            await _contexto.SaveChangesAsync();
        }

        public async Task RemoverAsync(Categoria categoria)
        {
            _contexto.Categorias.Remove(categoria);
            await _contexto.SaveChangesAsync();
        }
    }
}