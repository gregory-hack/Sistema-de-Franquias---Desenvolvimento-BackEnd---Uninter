using Franquias.Api.Models;
using Franquias.Api.Repositories;

namespace Franquias.Api.Services
{
    public class CobrancaService
    {
        private readonly CobrancaRepository _repositorio;
        private readonly VendaRepository _vendaRepositorio;

        public CobrancaService(CobrancaRepository repositorio, VendaRepository vendaRepositorio)
        {
            _repositorio = repositorio;
            _vendaRepositorio = vendaRepositorio;
        }

        public async Task<List<Cobranca>> ListarPorUnidadeAsync(int unidadeId)
        {
            return await _repositorio.ListarPorUnidadeAsync(unidadeId);
        }

        public async Task<Cobranca> CalcularRoyaltyAsync(int unidadeId, decimal percentual, DateTime inicioPeriodo, DateTime fimPeriodo, string periodoTexto)
        {
            var vendas = await _vendaRepositorio.ListarPorUnidadeEPeriodoAsync(unidadeId, inicioPeriodo, fimPeriodo);

            decimal faturamento = 0;
            foreach (var venda in vendas)
            {
                faturamento += venda.ValorTotal;
            }

            var valorCobranca = faturamento * (percentual / 100);

            var cobranca = new Cobranca
            {
                UnidadeFranqueadaId = unidadeId,
                Percentual = percentual,
                Periodo = periodoTexto,
                Faturamento = faturamento,
                ValorCobranca = valorCobranca,
                StatusPagamento = "Pendente"
            };

            await _repositorio.AdicionarAsync(cobranca);
            return await _repositorio.BuscarPorIdAsync(cobranca.Id) ?? cobranca;
        }

        public async Task MarcarComoPagoAsync(int id)
        {
            var cobranca = await _repositorio.BuscarPorIdAsync(id);
            if (cobranca == null)
            {
                throw new Exception("Cobrança não encontrada.");
            }

            cobranca.StatusPagamento = "Pago";
            await _repositorio.AtualizarAsync(cobranca);
        }
    }
}