
using Microsoft.AspNetCore.Mvc;
using Franquias.Api.DTOs;
using Franquias.Api.Models;
using Franquias.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Franquias.Api.Controllers
{
    
    [Authorize(Roles = "Administrador")]
    
    [ApiController]
    [Route("api/franqueadoras")]
    public class FranqueadoraController : ControllerBase
    {
        private readonly FranqueadoraService _service;

        public FranqueadoraController(FranqueadoraService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var franqueadoras = await _service.ListarTodosAsync();
            return Ok(franqueadoras);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var franqueadora = await _service.BuscarPorIdAsync(id);
            if (franqueadora == null)
            {
                return NotFound("Franqueadora não encontrada.");
            }
            return Ok(franqueadora);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(FranqueadoraDTO dto)
        {
            var franqueadora = new Franqueadora
            {
                NomeFantasia = dto.NomeFantasia,
                RazaoSocial = dto.RazaoSocial,
                CNPJ = dto.CNPJ,
                DataFundacao = dto.DataFundacao
            };

            await _service.AdicionarAsync(franqueadora);
            return Ok(franqueadora);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, FranqueadoraDTO dto)
        {
            var franqueadora = await _service.BuscarPorIdAsync(id);
            if (franqueadora == null)
            {
                return NotFound("Franqueadora não encontrada.");
            }

            franqueadora.NomeFantasia = dto.NomeFantasia;
            franqueadora.RazaoSocial = dto.RazaoSocial;
            franqueadora.CNPJ = dto.CNPJ;
            franqueadora.DataFundacao = dto.DataFundacao;

            await _service.AtualizarAsync(franqueadora);
            return Ok(franqueadora);
        }

        [HttpPut("{id}/inativar")]

       public async Task<IActionResult> Inativar(int id)
        {
            var franqueadora = await _service.BuscarPorIdAsync(id);
            if (franqueadora == null)
            {
                return NotFound("Franqueadora não encontrada.");
            }

            franqueadora.Status = "Inativo";
            await _service.AtualizarAsync(franqueadora);
            return Ok(franqueadora);
        }

    }

    
    
}
    

