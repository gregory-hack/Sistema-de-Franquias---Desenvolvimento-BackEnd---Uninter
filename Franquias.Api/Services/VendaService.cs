
using Franquias.Api.Data;
using Franquias.Api.Models;
using Franquias.Api.Repositories;

namespace Franquias.Api.Services
{
    public class VendaService
    {
        private readonly DBFranquias _contexto;
        private readonly VendaRepository _repositorio;
        private readonly UnidadeFranqueadaRepository _unidadeRepositorio;
        private readonly ProdutoServicoRepository _produtoRepositorio;
        private readonly IEstoqueService _estoqueService;

        public VendaService(
            DBFranquias contexto,
            VendaRepository repositorio,
            UnidadeFranqueadaRepository unidadeRepositorio,
            ProdutoServicoRepository produtoRepositorio,
            IEstoqueService estoqueService)
        {
            _contexto = contexto;
            _repositorio = repositorio;
            _unidadeRepositorio = unidadeRepositorio;
            _produtoRepositorio = produtoRepositorio;
            _estoqueService = estoqueService;
        }

        public async Task<List<Venda>> ListarPorUnidadeAsync(int unidadeId)
        {
            return await _repositorio.ListarPorUnidadeAsync(unidadeId);
        }

        public async Task<Venda?> BuscarPorIdAsync(int id)
        {
            return await _repositorio.BuscarPorIdAsync(id);
        }

        public async Task<List<Venda>> ListarPorUnidadeEPeriodoAsync(int unidadeId, DateTime inicio, DateTime fim)
        {
            return await _repositorio.ListarPorUnidadeEPeriodoAsync(unidadeId, inicio, fim);
        }

        public async Task RegistrarVendaAsync(Venda venda)
        {
            await using var transacao = await _contexto.Database.BeginTransactionAsync();

            try
            {
                // Regra: unidade precisa existir e estar ativa
                var unidade = await _unidadeRepositorio.BuscarPorIdAsync(venda.UnidadeFranqueadaId);
                if (unidade == null)
                {
                    throw new Exception("Unidade não encontrada.");
                }
                if (unidade.Situacao != "Ativa")
                {
                    throw new Exception("Unidade inativa não pode registrar vendas.");
                }

                // Regra: venda precisa ter pelo menos um item
                if (venda.Itens == null || venda.Itens.Count == 0)
                {
                    throw new Exception("A venda deve possuir pelo menos um item.");
                }

                decimal totalVenda = 0;

                foreach (var item in venda.Itens)
                {
                    var produto = await _produtoRepositorio.BuscarPorIdAsync(item.ProdutoServicoId);
                    if (produto == null)
                    {
                        throw new Exception("Produto/serviço não encontrado.");
                    }

                    item.PrecoUnitario = produto.PrecoBase;
                    item.Subtotal = item.PrecoUnitario * item.Quantidade;
                    totalVenda += item.Subtotal;

                    //  Lança erro automaticamente se o estoque ficar negativo
                    await _estoqueService.RegistrarSaidaAsync(venda.UnidadeFranqueadaId, item.ProdutoServicoId, item.Quantidade);
                }

                venda.ValorTotal = totalVenda;
                venda.DataVenda = DateTime.Now;

                await _repositorio.AdicionarAsync(venda);

                await transacao.CommitAsync();
            }
            catch
            {
                await transacao.RollbackAsync();
                throw;
            }
        }
    }
}