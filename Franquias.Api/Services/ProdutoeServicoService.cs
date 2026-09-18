
using Franquias.Api.Models;
using Franquias.Api.Repositories;

namespace Franquias.Api.Services
{
    public class ProdutoServicoService
    {
        private readonly ProdutoServicoRepository _repositorio;

        public ProdutoServicoService(ProdutoServicoRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<ProdutoServico>> ListarTodosAsync()
        {
            return await _repositorio.ListarTodosAsync();
        }

        public async Task<ProdutoServico?> BuscarPorIdAsync(int id)
        {
            return await _repositorio.BuscarPorIdAsync(id);
        }

        public async Task<List<ProdutoServico>> BuscarPorNomeAsync(string nome)
        {
            return await _repositorio.BuscarPorNomeAsync(nome);
        }

        public async Task<List<ProdutoServico>> BuscarPorCategoriaAsync(int categoriaId)
        {
            return await _repositorio.BuscarPorCategoriaAsync(categoriaId);
        }

        public async Task AdicionarAsync(ProdutoServico produto)
        {
            await _repositorio.AdicionarAsync(produto);
        }

        public async Task AtualizarAsync(ProdutoServico produto)
        {
            await _repositorio.AtualizarAsync(produto);
        }

        public async Task RemoverAsync(ProdutoServico produto)
        {
            await _repositorio.RemoverAsync(produto);
        }
    }
}