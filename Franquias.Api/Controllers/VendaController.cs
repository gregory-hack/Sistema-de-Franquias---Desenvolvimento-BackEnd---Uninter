
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Franquias.Api.DTOs;
using Franquias.Api.Models;
using Franquias.Api.Services;

namespace Franquias.Api.Controllers
{
    [Authorize(Roles = "Administrador,GestorUnidade,Operador")]
    [ApiController]
    [Route("api/vendas")]
    public class VendaController : ControllerBase
    {
        private readonly VendaService _service;

        public VendaController(VendaService service)
        {
            _service = service;
        }

        private static VendaRespostaDTO ParaResposta(Venda venda)
        {
            return new VendaRespostaDTO
            {
                Id = venda.Id,
                UnidadeFranqueadaId = venda.UnidadeFranqueadaId,
                UsuarioId = venda.UsuarioId,
                DataVenda = venda.DataVenda,
                ValorTotal = venda.ValorTotal,
                Itens = venda.Itens.Select(item => new ItemVendaRespostaDTO
                {
                    Id = item.Id,
                    ProdutoServicoId = item.ProdutoServicoId,
                    Quantidade = item.Quantidade,
                    PrecoUnitario = item.PrecoUnitario,
                    Subtotal = item.Subtotal
                }).ToList()
            };
        }

        [HttpGet("unidade/{unidadeId}")]
        public async Task<IActionResult> ListarPorUnidade(
            int unidadeId,
            bool decrescente = true,
            int pagina = 1,
            int tamanhoPagina = 10)
        {
            var vendas = await _service.ListarPorUnidadeAsync(unidadeId);

            vendas = decrescente
                ? vendas.OrderByDescending(v => v.DataVenda).ToList()
                : vendas.OrderBy(v => v.DataVenda).ToList();

            var total = vendas.Count;

            var itens = vendas
                .Skip((pagina - 1) * tamanhoPagina)
                .Take(tamanhoPagina)
                .Select(ParaResposta)
                .ToList();

            return Ok(new
            {
                Total = total,
                Pagina = pagina,
                TamanhoPagina = tamanhoPagina,
                Itens = itens
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var venda = await _service.BuscarPorIdAsync(id);

            if (venda == null)
            {
                return NotFound("Venda não encontrada.");
            }

            return Ok(ParaResposta(venda));
        }

        [HttpGet("unidade/{unidadeId}/periodo")]
        public async Task<IActionResult> ListarPorPeriodo(
            int unidadeId,
            DateTime inicio,
            DateTime fim)
        {
            var vendas = await _service.ListarPorUnidadeEPeriodoAsync(
                unidadeId,
                inicio,
                fim
            );

            return Ok(vendas.Select(ParaResposta).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(VendaDTO dto)
        {
            try
            {
                var venda = new Venda
                {
                    UnidadeFranqueadaId = dto.UnidadeFranqueadaId,
                    UsuarioId = dto.UsuarioId,
                    Itens = dto.Itens.Select(i => new ItemVenda
                    {
                        ProdutoServicoId = i.ProdutoServicoId,
                        Quantidade = i.Quantidade
                    }).ToList()
                };

                await _service.RegistrarVendaAsync(venda);

                var vendaCompleta = await _service.BuscarPorIdAsync(venda.Id);

                if (vendaCompleta == null)
                {
                    return StatusCode(
                        StatusCodes.Status500InternalServerError,
                        "A venda foi registrada, mas não pôde ser consultada."
                    );
                }

                return CreatedAtAction(
                    nameof(BuscarPorId),
                    new { id = venda.Id },
                    ParaResposta(vendaCompleta)
                );
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
    }
}