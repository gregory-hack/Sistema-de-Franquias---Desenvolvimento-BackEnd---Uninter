
using Franquias.Api.Models;
using Franquias.Api.Repositories;

namespace Franquias.Api.Services
{
    public class CategoriaService
    {
        private readonly CategoriaRepository _repositorio;

        public CategoriaService(CategoriaRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Categoria>> ListarTodosAsync()
        {
            return await _repositorio.ListarTodosAsync();
        }

        public async Task<Categoria?> BuscarPorIdAsync(int id)
        {
            return await _repositorio.BuscarPorIdAsync(id);
        }

        public async Task AdicionarAsync(Categoria categoria)
        {
            await _repositorio.AdicionarAsync(categoria);
        }

        public async Task AtualizarAsync(Categoria categoria)
        {
            await _repositorio.AtualizarAsync(categoria);
        }

        public async Task RemoverAsync(Categoria categoria)
        {
            await _repositorio.RemoverAsync(categoria);
        }
    }
}