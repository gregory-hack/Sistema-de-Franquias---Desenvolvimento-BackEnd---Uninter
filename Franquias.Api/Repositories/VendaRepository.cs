
using Microsoft.EntityFrameworkCore;
using Franquias.Api.Data;
using Franquias.Api.Models;

namespace Franquias.Api.Repositories
{
    public class VendaRepository
    {
        private readonly DBFranquias _contexto;

        public VendaRepository(DBFranquias contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Venda>> ListarTodosAsync()
        {
            return await _contexto.Vendas
                .Include(v => v.Itens)
                    .ThenInclude(i => i.ProdutoServico)
                .Include(v => v.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .ToListAsync();
        }

        public async Task<Venda?> BuscarPorIdAsync(int id)
        {
            return await _contexto.Vendas
                .Include(v => v.Itens)
                    .ThenInclude(i => i.ProdutoServico)
                .Include(v => v.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<Venda>> ListarPorUnidadeAsync(int unidadeId)
        {
            return await _contexto.Vendas
                .Include(v => v.Itens)
                    .ThenInclude(i => i.ProdutoServico)
                .Include(v => v.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .Where(v => v.UnidadeFranqueadaId == unidadeId)
                .ToListAsync();
        }

        public async Task<List<Venda>> ListarPorUnidadeEPeriodoAsync(int unidadeId, DateTime inicio, DateTime fim)
        {
            return await _contexto.Vendas
                .Include(v => v.Itens)
                    .ThenInclude(i => i.ProdutoServico)
                .Include(v => v.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .Where(v => v.UnidadeFranqueadaId == unidadeId && v.DataVenda >= inicio && v.DataVenda <= fim)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Venda venda)
        {
            _contexto.Vendas.Add(venda);
            await _contexto.SaveChangesAsync();
        }
    }
}