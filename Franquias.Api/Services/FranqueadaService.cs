
using Franquias.Api.Models;
using Franquias.Api.Repositories;

namespace Franquias.Api.Services
{
    public class FranqueadoService
    {
        private readonly FranqueadoRepository _repositorio;

        public FranqueadoService(FranqueadoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Franqueado>> ListarTodosAsync()
        {
            return await _repositorio.ListarTodosAsync();
        }

        public async Task<Franqueado?> BuscarPorIdAsync(int id)
        {
            return await _repositorio.BuscarPorIdAsync(id);
        }

        public async Task AdicionarAsync(Franqueado franqueado)
        {
            await _repositorio.AdicionarAsync(franqueado);
        }

        public async Task AtualizarAsync(Franqueado franqueado)
        {
            await _repositorio.AtualizarAsync(franqueado);
        }

        public async Task RemoverAsync(Franqueado franqueado)
        {
            await _repositorio.RemoverAsync(franqueado);
        }
    }
}