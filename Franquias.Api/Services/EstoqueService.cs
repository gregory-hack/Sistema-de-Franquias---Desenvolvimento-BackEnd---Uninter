
using Franquias.Api.Models;
using Franquias.Api.Repositories;

namespace Franquias.Api.Services
{
    public class EstoqueService : IEstoqueService
    {
        private readonly EstoqueRepository _repositorio;

        public EstoqueService(EstoqueRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Estoque>> ListarPorUnidadeAsync(int unidadeId)
        {
            return await _repositorio.ListarPorUnidadeAsync(unidadeId);
        }

        public async Task<List<Estoque>> ListarAbaixoDoMinimoAsync(int unidadeId)
        {
            return await _repositorio.ListarAbaixoDoMinimoAsync(unidadeId);
        }

        public async Task RegistrarEntradaAsync(int unidadeId, int produtoId, int quantidade)
        {
            var estoque = await _repositorio.BuscarPorUnidadeEProdutoAsync(unidadeId, produtoId);

            if (estoque == null)
            {
                estoque = new Estoque
                {
                    UnidadeFranqueadaId = unidadeId,
                    ProdutoServicoId = produtoId,
                    QuantidadeAtual = quantidade,
                    QuantidadeMinima = 0
                };
                await _repositorio.AdicionarAsync(estoque);
            }
            else
            {
                estoque.QuantidadeAtual += quantidade;
                await _repositorio.AtualizarAsync(estoque);
            }
        }

        public async Task RegistrarSaidaAsync(int unidadeId, int produtoId, int quantidade)
        {
            var estoque = await _repositorio.BuscarPorUnidadeEProdutoAsync(unidadeId, produtoId);

            if (estoque == null)
            {
                throw new Exception("Não existe estoque cadastrado para esse produto nessa unidade.");
            }

            if (estoque.QuantidadeAtual - quantidade < 0)
            {
                throw new Exception("Estoque insuficiente para realizar essa saída.");
            }

            estoque.QuantidadeAtual -= quantidade;
            await _repositorio.AtualizarAsync(estoque);
        }
    }
}