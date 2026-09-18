
using Franquias.Api.Models;
using Franquias.Api.Repositories;

namespace Franquias.Api.Services
{
    public class UnidadeFranqueadaService
    {
        private readonly UnidadeFranqueadaRepository _repositorio;

        public UnidadeFranqueadaService(UnidadeFranqueadaRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<UnidadeFranqueada>> ListarTodosAsync()
        {
            return await _repositorio.ListarTodosAsync();
        }

        public async Task<UnidadeFranqueada?> BuscarPorIdAsync(int id)
        {
            return await _repositorio.BuscarPorIdAsync(id);
        }

        public async Task AdicionarAsync(UnidadeFranqueada unidade)
        {
            var existente = await _repositorio.BuscarPorCnpjAsync(unidade.CNPJ);
            if (existente != null)
            {
                throw new Exception("Já existe uma unidade cadastrada com esse CNPJ.");
            }

            await _repositorio.AdicionarAsync(unidade);
        }

        public async Task AtualizarAsync(UnidadeFranqueada unidade)
        {
            await _repositorio.AtualizarAsync(unidade);
        }

        public async Task InativarAsync(int id)
        {
            var unidade = await _repositorio.BuscarPorIdAsync(id);
            if (unidade == null)
            {
                throw new Exception("Unidade não encontrada.");
            }

            unidade.Situacao = "Inativa";
            await _repositorio.AtualizarAsync(unidade);
        }
    }
}