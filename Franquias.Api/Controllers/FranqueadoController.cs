
using Microsoft.AspNetCore.Mvc;
using Franquias.Api.DTOs;
using Franquias.Api.Models;
using Franquias.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Franquias.Api.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/franqueados")]
    public class FranqueadoController : ControllerBase
    {
        private readonly FranqueadoService _service;

        public FranqueadoController(FranqueadoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var franqueados = await _service.ListarTodosAsync();
            return Ok(franqueados);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var franqueado = await _service.BuscarPorIdAsync(id);
            if (franqueado == null)
            {
                return NotFound("Franqueado não encontrado.");
            }
            return Ok(franqueado);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(FranqueadoDTO dto)
        {
            var franqueado = new Franqueado
            {
                Nome = dto.Nome,
                CPF = dto.CPF,
                Email = dto.Email,
                Telefone = dto.Telefone,
                UnidadeFranqueadaId = dto.UnidadeFranqueadaId
            };

            await _service.AdicionarAsync(franqueado);
            var franqueadoCompleto = await _service.BuscarPorIdAsync(franqueado.Id);
            return Ok(franqueadoCompleto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, FranqueadoDTO dto)
        {
            var franqueado = await _service.BuscarPorIdAsync(id);
            if (franqueado == null)
            {
                return NotFound("Franqueado não encontrado.");
            }

            franqueado.Nome = dto.Nome;
            franqueado.CPF = dto.CPF;
            franqueado.Email = dto.Email;
            franqueado.Telefone = dto.Telefone;

            await _service.AtualizarAsync(franqueado);
            return Ok(franqueado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var franqueado = await _service.BuscarPorIdAsync(id);
            if (franqueado == null)
            {
                return NotFound("Franqueado não encontrado.");
            }

            await _service.RemoverAsync(franqueado);
            return Ok("Franqueado removido com sucesso.");
        }
    }
}