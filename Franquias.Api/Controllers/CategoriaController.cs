
using Microsoft.AspNetCore.Mvc;
using Franquias.Api.DTOs;
using Franquias.Api.Models;
using Franquias.Api.Services;
using Microsoft.AspNetCore.Authorization;
namespace Franquias.Api.Controllers
{

    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/categorias")]
    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _service;

        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var categorias = await _service.ListarTodosAsync();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var categoria = await _service.BuscarPorIdAsync(id);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada.");
            }
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(CategoriaDTO dto)
        {
            var categoria = new Categoria { Nome = dto.Nome };
            await _service.AdicionarAsync(categoria);
            return Ok(categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, CategoriaDTO dto)
        {
            var categoria = await _service.BuscarPorIdAsync(id);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada.");
            }

            categoria.Nome = dto.Nome;
            await _service.AtualizarAsync(categoria);
            return Ok(categoria);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Remover(int id)
        {
            var categoria = await _service.BuscarPorIdAsync(id);
            if (categoria == null)
            {
                return NotFound("Categoria não encontrada.");
            }

            await _service.RemoverAsync(categoria);
            return Ok("Categoria removida com sucesso.");
        }
    }
}