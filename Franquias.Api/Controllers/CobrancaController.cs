
using Microsoft.AspNetCore.Mvc;
using Franquias.Api.DTOs;
using Franquias.Api.Services;
using Microsoft.AspNetCore.Authorization;
namespace Franquias.Api.Controllers
{
    [Authorize(Roles = "Administrador,GestorUnidade")] 
    [ApiController]
    [Route("api/royalties")]
    public class CobrancaController : ControllerBase
    {
        private readonly CobrancaService _service;

        public CobrancaController(CobrancaService service)
        {
            _service = service;
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> GerarCobranca(CobrancaDTO dto)
        {
            if (dto.InicioPeriodo >= dto.FimPeriodo)
            {
                return BadRequest("O início do período deve ser anterior ao fim do período.");
            }

            var cobranca = await _service.CalcularRoyaltyAsync(
                dto.UnidadeFranqueadaId,
                dto.Percentual,
                dto.InicioPeriodo,
                dto.FimPeriodo,
                dto.PeriodoTexto
            );

            return Ok(cobranca);
        }

        [HttpGet("unidade/{unidadeId}")]
        public async Task<IActionResult> ListarPorUnidade(int unidadeId, string? status)
        {
            var cobrancas = await _service.ListarPorUnidadeAsync(unidadeId);

            if (!string.IsNullOrEmpty(status))
            {
                cobrancas = cobrancas.Where(c => c.StatusPagamento == status).ToList();
            }

            return Ok(cobrancas);
        }

        [HttpPut("{id}/pagar")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> MarcarComoPago(int id)
        {
            try
            {
                await _service.MarcarComoPagoAsync(id);
                return Ok("Cobrança marcada como paga.");
            }
            catch (Exception erro)
            {
                return NotFound(erro.Message);
            }
        }
    }
}