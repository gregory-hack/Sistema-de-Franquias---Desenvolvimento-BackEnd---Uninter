
using Franquias.Api.Models;

namespace Franquias.Api.Services
{
    public interface IEstoqueService
    {
        Task<List<Estoque>> ListarPorUnidadeAsync(int unidadeId);
        Task<List<Estoque>> ListarAbaixoDoMinimoAsync(int unidadeId);
        Task RegistrarEntradaAsync(int unidadeId, int produtoId, int quantidade);
        Task RegistrarSaidaAsync(int unidadeId, int produtoId, int quantidade);
    }
}