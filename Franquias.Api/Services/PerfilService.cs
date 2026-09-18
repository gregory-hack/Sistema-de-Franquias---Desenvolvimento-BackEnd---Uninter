
using Franquias.Api.Models;
using Franquias.Api.Repositories;

namespace Franquias.Api.Services
{
    public class PerfilService
    {
        private readonly PerfilRepository _repositorio;

        public PerfilService(PerfilRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Perfil>> ListarTodosAsync()
        {
            return await _repositorio.ListarTodosAsync();
        }

        public async Task<Perfil?> BuscarPorIdAsync(int id)
        {
            return await _repositorio.BuscarPorIdAsync(id);
        }

        public async Task AdicionarAsync(Perfil perfil)
        {
            await _repositorio.AdicionarAsync(perfil);
        }

        public async Task AtualizarAsync(Perfil perfil)
        {
            await _repositorio.AtualizarAsync(perfil);
        }

        public async Task RemoverAsync(Perfil perfil)
        {
            await _repositorio.RemoverAsync(perfil);
        }
    }
}