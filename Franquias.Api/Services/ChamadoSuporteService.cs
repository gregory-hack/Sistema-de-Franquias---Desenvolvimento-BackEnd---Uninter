
using Franquias.Api.Models;
using Franquias.Api.Repositories;

namespace Franquias.Api.Services
{
    public class ChamadoSuporteService
    {
        private readonly ChamadoSuporteRepository _repositorio;

        public ChamadoSuporteService(ChamadoSuporteRepository repositorio)
        {
            _repositorio = repositorio;
        }

        public async Task<List<ChamadoSuporte>> ListarTodosAsync()
        {
            return await _repositorio.ListarTodosAsync();
        }

        public async Task<ChamadoSuporte?> BuscarPorIdAsync(int id)
        {
            return await _repositorio.BuscarPorIdAsync(id);
        }

        public async Task<List<ChamadoSuporte>> ListarPorStatusAsync(string status)
        {
            return await _repositorio.ListarPorStatusAsync(status);
        }

        public async Task<List<ChamadoSuporte>> ListarPorUnidadeAsync(int unidadeId)
        {
            return await _repositorio.ListarPorUnidadeAsync(unidadeId);
        }

        public async Task AbrirChamadoAsync(ChamadoSuporte chamado)
        {
            chamado.Status = "Aberto";
            chamado.DataAbertura = DateTime.Now;
            await _repositorio.AdicionarAsync(chamado);
        }

        public async Task EncerrarChamadoAsync(int id)
        {
            var chamado = await _repositorio.BuscarPorIdAsync(id);
            if (chamado == null)
            {
                throw new Exception("Chamado não encontrado.");
            }

            chamado.Status = "Encerrado";
            chamado.DataEncerramento = DateTime.Now;
            await _repositorio.AtualizarAsync(chamado);
        }

        public async Task AtualizarAsync(ChamadoSuporte chamado)
        {
            await _repositorio.AtualizarAsync(chamado);
        }
    }
}