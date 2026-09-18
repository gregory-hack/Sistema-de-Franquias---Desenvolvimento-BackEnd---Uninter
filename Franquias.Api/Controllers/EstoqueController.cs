
using Microsoft.AspNetCore.Mvc;
using Franquias.Api.DTOs;
using Franquias.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Franquias.Api.Controllers
{
    [Authorize(Roles = "Administrador,GestorUnidade,Operador")] 
    [ApiController]
    [Route("api/estoques")]
    public class EstoqueController : ControllerBase
    {
        private readonly IEstoqueService _service;

        public EstoqueController(IEstoqueService service)
        {
            _service = service;
        }

        [HttpGet("unidade/{unidadeId}")]
        public async Task<IActionResult> ListarPorUnidade(int unidadeId)
        {
            var estoques = await _service.ListarPorUnidadeAsync(unidadeId);
            return Ok(estoques);
        }

        [HttpGet("unidade/{unidadeId}/abaixo-do-minimo")]
        public async Task<IActionResult> ListarAbaixoDoMinimo(int unidadeId)
        {
            var estoques = await _service.ListarAbaixoDoMinimoAsync(unidadeId);
            return Ok(estoques);
        }

        [HttpPost("entrada")]
        public async Task<IActionResult> RegistrarEntrada(EstoqueMovimentacaoDTO dto)
        {
            try
            {
                await _service.RegistrarEntradaAsync(dto.UnidadeFranqueadaId, dto.ProdutoServicoId, dto.Quantidade);
                return Ok("Entrada de estoque registrada com sucesso.");
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPost("saida")]
        public async Task<IActionResult> RegistrarSaida(EstoqueMovimentacaoDTO dto)
        {
            try
            {
                await _service.RegistrarSaidaAsync(dto.UnidadeFranqueadaId, dto.ProdutoServicoId, dto.Quantidade);
                return Ok("Saída de estoque registrada com sucesso.");
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}