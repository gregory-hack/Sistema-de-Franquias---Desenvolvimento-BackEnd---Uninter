
using Franquias.Api.Models;
using Franquias.Api.Repositories;

namespace Franquias.Api.Services
{
    public class FornecedorService
    {
        private readonly FornecedorRepository _repositorio;

        public FornecedorService(FornecedorRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<Fornecedor>> ListarTodosAsync()
        {
            return await _repositorio.ListarTodosAsync();
        }

        public async Task<Fornecedor?> BuscarPorIdAsync(int id)
        {
            return await _repositorio.BuscarPorIdAsync(id);
        }

        public async Task AdicionarAsync(Fornecedor fornecedor)
        {
            var existente = await _repositorio.BuscarPorCnpjAsync(fornecedor.CNPJ);
            if (existente != null)
            {
                throw new Exception("Já existe um fornecedor cadastrado com esse CNPJ.");
            }

            await _repositorio.AdicionarAsync(fornecedor);
        }

        public async Task AtualizarAsync(Fornecedor fornecedor)
        {
            await _repositorio.AtualizarAsync(fornecedor);
        }

        public async Task RemoverAsync(Fornecedor fornecedor)
        {
            await _repositorio.RemoverAsync(fornecedor);
        }
    }
}