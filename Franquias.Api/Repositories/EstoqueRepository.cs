
using Microsoft.EntityFrameworkCore;
using Franquias.Api.Data;
using Franquias.Api.Models;

namespace Franquias.Api.Repositories
{
    public class EstoqueRepository
    {
        private readonly DBFranquias _contexto;

        public EstoqueRepository(DBFranquias contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Estoque>> ListarTodosAsync()
        {
            return await _contexto.Estoques
                .Include(e => e.ProdutoServico)
                    .ThenInclude(p => p!.Categoria)
                .Include(e => e.ProdutoServico)
                    .ThenInclude(p => p!.Fornecedor)
                .Include(e => e.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .ToListAsync();
        }

        public async Task<Estoque?> BuscarPorIdAsync(int id)
        {
            return await _contexto.Estoques
                .Include(e => e.ProdutoServico)
                    .ThenInclude(p => p!.Categoria)
                .Include(e => e.ProdutoServico)
                    .ThenInclude(p => p!.Fornecedor)
                .Include(e => e.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Estoque?> BuscarPorUnidadeEProdutoAsync(int unidadeId, int produtoId)
        {
            return await _contexto.Estoques
                .FirstOrDefaultAsync(e => e.UnidadeFranqueadaId == unidadeId && e.ProdutoServicoId == produtoId);
        }

        public async Task<List<Estoque>> ListarPorUnidadeAsync(int unidadeId)
        {
            return await _contexto.Estoques
                .Include(e => e.ProdutoServico)
                    .ThenInclude(p => p!.Categoria)
                .Include(e => e.ProdutoServico)
                    .ThenInclude(p => p!.Fornecedor)
                .Include(e => e.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .Where(e => e.UnidadeFranqueadaId == unidadeId)
                .ToListAsync();
        }

        public async Task<List<Estoque>> ListarAbaixoDoMinimoAsync(int unidadeId)
        {
            return await _contexto.Estoques
                .Include(e => e.ProdutoServico)
                    .ThenInclude(p => p!.Categoria)
                .Include(e => e.ProdutoServico)
                    .ThenInclude(p => p!.Fornecedor)
                .Include(e => e.UnidadeFranqueada)
                    .ThenInclude(u => u!.Franqueadora)
                .Where(e => e.UnidadeFranqueadaId == unidadeId && e.QuantidadeAtual < e.QuantidadeMinima)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Estoque estoque)
        {
            _contexto.Estoques.Add(estoque);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Estoque estoque)
        {
            _contexto.Estoques.Update(estoque);
            await _contexto.SaveChangesAsync();
        }

        public async Task RemoverAsync(Estoque estoque)
        {
            _contexto.Estoques.Remove(estoque);
            await _contexto.SaveChangesAsync();
        }
    }
}