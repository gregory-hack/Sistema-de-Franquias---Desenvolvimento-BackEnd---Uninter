
using Franquias.Api.Models;
using Franquias.Api.Repositories;

namespace Franquias.Api.Services
{
    public class FranqueadoraService
    {
        private readonly FranqueadoraRepository _repositorio;

        public FranqueadoraService(FranqueadoraRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Franqueadora>> ListarTodosAsync()
        {
            return await _repositorio.ListarTodosAsync();
        }

        public async Task<Franqueadora?> BuscarPorIdAsync(int id)
        {
            return await _repositorio.BuscarPorIdAsync(id);
        }

        public async Task AdicionarAsync(Franqueadora franqueadora)
        {
            await _repositorio.AdicionarAsync(franqueadora);
        }

        public async Task AtualizarAsync(Franqueadora franqueadora)
        {
            await _repositorio.AtualizarAsync(franqueadora);
        }

        public async Task RemoverAsync(Franqueadora franqueadora)
        {
            await _repositorio.RemoverAsync(franqueadora);
        }
    }
}