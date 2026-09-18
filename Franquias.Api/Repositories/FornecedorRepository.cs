
using Microsoft.EntityFrameworkCore;
using Franquias.Api.Data;
using Franquias.Api.Models;

namespace Franquias.Api.Repositories
{
    public class FornecedorRepository
    {
        private readonly DBFranquias _contexto;

        public FornecedorRepository(DBFranquias contexto)
        {
            _contexto = contexto;
        }

        public async Task<List<Fornecedor>> ListarTodosAsync()
        {
            return await _contexto.Fornecedores.ToListAsync();
        }

        public async Task<Fornecedor?> BuscarPorIdAsync(int id)
        {
            return await _contexto.Fornecedores.FindAsync(id);
        }

        public async Task<Fornecedor?> BuscarPorCnpjAsync(string cnpj)
        {
            return await _contexto.Fornecedores.FirstOrDefaultAsync(f => f.CNPJ == cnpj);
        }

        public async Task AdicionarAsync(Fornecedor fornecedor)
        {
            _contexto.Fornecedores.Add(fornecedor);
            await _contexto.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Fornecedor fornecedor)
        {
            _contexto.Fornecedores.Update(fornecedor);
            await _contexto.SaveChangesAsync();
        }

        public async Task RemoverAsync(Fornecedor fornecedor)
        {
            _contexto.Fornecedores.Remove(fornecedor);
            await _contexto.SaveChangesAsync();
        }
    }
}