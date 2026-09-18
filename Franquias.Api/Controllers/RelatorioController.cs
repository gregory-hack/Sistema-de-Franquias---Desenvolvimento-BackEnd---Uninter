
using Microsoft.AspNetCore.Mvc;
using Franquias.Api.Services;
using Microsoft.AspNetCore.Authorization;
namespace Franquias.Api.Controllers
{
    [Authorize(Roles = "Administrador,GestorUnidade")]
    [ApiController]
    [Route("api/relatorios")]
    public class RelatorioController : ControllerBase
    {
        private readonly RelatorioService _service;

        public RelatorioController(RelatorioService service)
        {
            _service = service;
        }

        [HttpGet("faturamento")]
        public async Task<IActionResult> Faturamento(DateTime inicio, DateTime fim)
        {
            var resultado = await _service.FaturamentoPorUnidadeAsync(inicio, fim);
            return Ok(resultado);
        }

        [HttpGet("ranking-unidades")]
        public async Task<IActionResult> RankingUnidades(DateTime inicio, DateTime fim)
        {
            var resultado = await _service.RankingUnidadesAsync(inicio, fim);
            return Ok(resultado);
        }

        [HttpGet("royalties-totais")]
        public async Task<IActionResult> RoyaltiesTotais()
        {
            var total = await _service.TotalRoyaltiesGeradosAsync();
            return Ok(new { TotalRoyaltiesGerados = total });
        }

        [HttpGet("produtos-mais-vendidos")]
        public async Task<IActionResult> ProdutosMaisVendidos(int top = 10)
        {
            var resultado = await _service.ProdutosMaisVendidosAsync(top);
            return Ok(resultado);
        }

        [HttpGet("estoque-critico")]
        public async Task<IActionResult> EstoqueCritico()
        {
            var resultado = await _service.EstoqueCriticoAsync();
            return Ok(resultado);
        }

        [HttpGet("chamados-por-status")]
        public async Task<IActionResult> ChamadosPorStatus()
        {
            var resultado = await _service.ChamadosPorStatusAsync();
            return Ok(resultado);
        }
    }
}