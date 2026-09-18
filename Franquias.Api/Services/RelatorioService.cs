
using Microsoft.EntityFrameworkCore;
using Franquias.Api.Data;

namespace Franquias.Api.Services
{
    public class RelatorioService
    {
        private readonly DBFranquias _contexto;

        public RelatorioService(DBFranquias contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<object>> FaturamentoPorUnidadeAsync(DateTime inicio, DateTime fim)
        {
            var resultado = await _contexto.Vendas
                .Where(v => v.DataVenda >= inicio && v.DataVenda <= fim)
                .GroupBy(v => v.UnidadeFranqueadaId)
                .Select(g => new
                {
                    UnidadeFranqueadaId = g.Key,
                    Faturamento = g.Sum(v => v.ValorTotal)
                })
                .ToListAsync();

            return resultado.Cast<object>().ToList();
        }

        public async Task<List<object>> RankingUnidadesAsync(DateTime inicio, DateTime fim)
        {
            var resultado = await _contexto.Vendas
                .Where(v => v.DataVenda >= inicio && v.DataVenda <= fim)
                .GroupBy(v => v.UnidadeFranqueadaId)
                .Select(g => new
                {
                    UnidadeFranqueadaId = g.Key,
                    Faturamento = g.Sum(v => v.ValorTotal)
                })
                .OrderByDescending(r => r.Faturamento)
                .ToListAsync();

            return resultado.Cast<object>().ToList();
        }

        public async Task<decimal> TotalRoyaltiesGeradosAsync()
        {
            return await _contexto.Cobrancas.SumAsync(c => c.ValorCobranca);
        }

        public async Task<List<object>> ProdutosMaisVendidosAsync(int top = 10)
        {
            var resultado = await _contexto.ItensVenda
                .Include(i => i.ProdutoServico)
                .GroupBy(i => new { i.ProdutoServicoId, i.ProdutoServico!.Nome })
                .Select(g => new
                {
                    ProdutoServicoId = g.Key.ProdutoServicoId,
                    Nome = g.Key.Nome,
                    QuantidadeVendida = g.Sum(i => i.Quantidade)
                })
                .OrderByDescending(p => p.QuantidadeVendida)
                .Take(top)
                .ToListAsync();

            return resultado.Cast<object>().ToList();
        }

        public async Task<List<object>> EstoqueCriticoAsync()
        {
            var resultado = await _contexto.Estoques
                .Include(e => e.ProdutoServico)
                .Include(e => e.UnidadeFranqueada)
                .Where(e => e.QuantidadeAtual <= e.QuantidadeMinima)
                .Select(e => new
                {
                    e.Id,
                    Produto = e.ProdutoServico!.Nome,
                    Unidade = e.UnidadeFranqueada!.Nome,
                    e.QuantidadeAtual,
                    e.QuantidadeMinima
                })
                .ToListAsync();

            return resultado.Cast<object>().ToList();
        }

        public async Task<List<object>> ChamadosPorStatusAsync()
        {
            var resultado = await _contexto.ChamadosSuporte
                .GroupBy(c => c.Status)
                .Select(g => new
                {
                    Status = g.Key,
                    Quantidade = g.Count()
                })
                .ToListAsync();

            return resultado.Cast<object>().ToList();
        }
    }
}