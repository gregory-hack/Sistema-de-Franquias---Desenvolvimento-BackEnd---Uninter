
using Microsoft.AspNetCore.Mvc;
using Franquias.Api.DTOs;
using Franquias.Api.Models;
using Franquias.Api.Services;
using Microsoft.AspNetCore.Authorization;

namespace Franquias.Api.Controllers
{
    [Authorize(Roles = "Administrador")]
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var usuarios = await _usuarioService.ListarTodosAsync();

            var resposta = usuarios.Select(u => new UsuarioRespostaDTO
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email,
                Ativo = u.Ativo,
                PerfilId = u.PerfilId
            }).ToList();

            return Ok(resposta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> BuscarPorId(int id)
        {
            var usuario = await _usuarioService.BuscarPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            var resposta = new UsuarioRespostaDTO
            {
                Id = usuario.Id,
                Nome = usuario.Nome,
                Email = usuario.Email,
                Ativo = usuario.Ativo,
                PerfilId = usuario.PerfilId
            };

            return Ok(resposta);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(UsuarioCadastroDTO dto)
        {
            try
            {
                var usuario = new Usuario
                {
                    Nome = dto.Nome,
                    Email = dto.Email,
                    PerfilId = dto.PerfilId
                };

                await _usuarioService.AdicionarAsync(usuario, dto.Senha);
                return Ok("Usuário cadastrado com sucesso.");
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }

        [HttpPut("{id}/inativar")]
        public async Task<IActionResult> Inativar(int id)
        {
            var usuario = await _usuarioService.BuscarPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            usuario.Ativo = false;
            await _usuarioService.AtualizarAsync(usuario);
            return Ok("Usuário inativado com sucesso.");
        }

        [HttpPut("{id}/ativar")]
        public async Task<IActionResult> Ativar(int id)
        {
            var usuario = await _usuarioService.BuscarPorIdAsync(id);
            if (usuario == null)
            {
                return NotFound("Usuário não encontrado.");
            }

            usuario.Ativo = true;
            await _usuarioService.AtualizarAsync(usuario);
            return Ok("Usuário ativado com sucesso.");
        }
    }
}