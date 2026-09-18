
using Microsoft.EntityFrameworkCore;
using Franquias.Api.Data;
using Franquias.Api.Models;

namespace Franquias.Api.Repositories
{
    public class ProdutoServicoRepository
    {
        private readonly DBFranquias _contexto;

        public ProdutoServicoRepository(DBFranquias contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<ProdutoServico>> ListarTodosAsync()
        {
            return await _contexto.ProdutosServicos
                .Include(p => p.Categoria)
                .Include(p => p.Fornecedor)
                .ToListAsync();
        }

        public async Task<ProdutoServico?> BuscarPorIdAsync(int id)
        {
            return await _contexto.ProdutosServicos
                .Include(p => p.Categoria)
                .Include(p => p.Fornecedor)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<ProdutoServico>> BuscarPorNomeAsync(string nome)
        {
            return await _contexto.ProdutosServicos
                .Where(p => p.Nome.Contains(nome))
                .ToListAsync();
        }

        public async Task<List<ProdutoServico>> BuscarPorCategoriaAsync(int categoriaId)
        {
            return await _contexto.ProdutosServicos
                .Where(p => p.CategoriaId == categoriaId)
                .ToListAsync();
        }

        public async Task AdicionarAsync(ProdutoServico produto)
        {
            _contexto.ProdutosServicos.Add(produto);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarAsync(ProdutoServico produto)
        {
            _contexto.ProdutosServicos.Update(produto);
            await _contexto.SaveChangesAsync();
        }

        public async Task RemoverAsync(ProdutoServico produto)
        {
            _contexto.ProdutosServicos.Remove(produto);
            await _contexto.SaveChangesAsync();
        }
    }
}