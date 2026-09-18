
using Microsoft.AspNetCore.Mvc;
using Franquias.Api.DTOs;
using Franquias.Api.Models;
using Franquias.Api.Services;
using Microsoft.AspNetCore.Authorization;
namespace Franquias.Api.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/fornecedores")]
    public class FornecedorController : ControllerBase
    {
        private readonly FornecedorService _service;

        public FornecedorController(FornecedorService service)
        {
            _service = service;
        }

       [HttpGet]
public async Task<IActionResult> Listar(string? nome, string? cnpj, string? status)
{
    var fornecedores = await _service.ListarTodosAsync();

    if (!string.IsNullOrEmpty(nome))
    {
        fornecedores = fornecedores.Where(f => f.Nome.Contains(nome, StringComparison.OrdinalIgnoreCase)).ToList();
    }

    if (!string.IsNullOrEmpty(cnpj))
    {
        fornecedores = fornecedores.Where(f => f.CNPJ == cnpj).ToList();
    }

    if (!string.IsNullOrEmpty(status))
    {
        fornecedores = fornecedores.Where(f => f.Status == status).ToList();
    }

    return Ok(fornecedores);
}

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var fornecedor = await _service.BuscarPorIdAsync(id);
            if (fornecedor == null)
            {
                return NotFound("Fornecedor não encontrado.");
            }
            return Ok(fornecedor);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(FornecedorDTO dto)
        {
            try
            {
                var fornecedor = new Fornecedor
                {
                    Nome = dto.Nome,
                    CNPJ = dto.CNPJ,
                    Telefone = dto.Telefone,
                    Email = dto.Email,
                    Status = "Ativo"
                };

                await _service.AdicionarAsync(fornecedor);
                return Ok(fornecedor);
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, FornecedorDTO dto)
        {
            var fornecedor = await _service.BuscarPorIdAsync(id);
            if (fornecedor == null)
            {
                return NotFound("Fornecedor não encontrado.");
            }

            fornecedor.Nome = dto.Nome;
            fornecedor.Telefone = dto.Telefone;
            fornecedor.Email = dto.Email;

            await _service.AtualizarAsync(fornecedor);
            return Ok(fornecedor);
        }

        [HttpPut("{id}/inativar")]
        public async Task<IActionResult> Inativar(int id)
        {
            var fornecedor = await _service.BuscarPorIdAsync(id);
            if (fornecedor == null)
            {
                return NotFound("Fornecedor não encontrado.");
            }

            fornecedor.Status = "Inativo";
            await _service.AtualizarAsync(fornecedor);
            return Ok(fornecedor);
        }
    }
}