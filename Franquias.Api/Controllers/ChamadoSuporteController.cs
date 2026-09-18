
using Microsoft.AspNetCore.Mvc;
using Franquias.Api.DTOs;
using Franquias.Api.Models;
using Franquias.Api.Services;
using Microsoft.AspNetCore.Authorization;
namespace Franquias.Api.Controllers
{
    [Authorize(Roles = "Administrador,GestorUnidade,Operador")]
    [ApiController]
    [Route("api/chamados")]
    public class ChamadoSuporteController : ControllerBase
    {
        private readonly ChamadoSuporteService _service;

        public ChamadoSuporteController(ChamadoSuporteService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar(string? status, int? unidadeId, string? prioridade)
        {
            var chamados = await _service.ListarTodosAsync();

            if (!string.IsNullOrEmpty(status))
            {
                chamados = chamados.Where(c => c.Status == status).ToList();
            }
            if (unidadeId.HasValue)
            {
                chamados = chamados.Where(c => c.UnidadeFranqueadaId == unidadeId.Value).ToList();
            }
            if (!string.IsNullOrEmpty(prioridade))
            {
                chamados = chamados.Where(c => c.Prioridade == prioridade).ToList();
            }

            return Ok(chamados);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var chamado = await _service.BuscarPorIdAsync(id);
            if (chamado == null)
            {
                return NotFound("Chamado não encontrado.");
            }
            return Ok(chamado);
        }

        [HttpPost]
        public async Task<IActionResult> Abrir(ChamadoSuporteDTO dto)
        {
            var chamado = new ChamadoSuporte
            {
                UnidadeFranqueadaId = dto.UnidadeFranqueadaId,
                UsuarioId = dto.UsuarioId,
                Categoria = dto.Categoria,
                Prioridade = dto.Prioridade,
                Descricao = dto.Descricao
            };

            await _service.AbrirChamadoAsync(chamado);
            var chamadoCompleto = await _service.BuscarPorIdAsync(chamado.Id);
            return Ok(chamadoCompleto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, ChamadoSuporteDTO dto)
        {
            var chamado = await _service.BuscarPorIdAsync(id);
            if (chamado == null)
            {
                return NotFound("Chamado não encontrado.");
            }

            chamado.Categoria = dto.Categoria;
            chamado.Prioridade = dto.Prioridade;
            chamado.Descricao = dto.Descricao;

            await _service.AtualizarAsync(chamado);
            return Ok(chamado);
        }

        [HttpPut("{id}/encerrar")]
        public async Task<IActionResult> Encerrar(int id)
        {
            try
            {
                await _service.EncerrarChamadoAsync(id);
                return Ok("Chamado encerrado com sucesso.");
            }
            catch (Exception erro)
            {
                return NotFound(erro.Message);
            }
        }
    }
}