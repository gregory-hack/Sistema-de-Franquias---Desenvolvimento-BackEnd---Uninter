
using Microsoft.AspNetCore.Mvc;
using Franquias.Api.DTOs;
using Franquias.Api.Services;

namespace Franquias.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioService _usuarioService;
        private readonly TokenService _tokenService;

        public AuthController(UsuarioService usuarioService, TokenService tokenService)
        {
            _usuarioService = usuarioService;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {
            var usuarios = await _usuarioService.ListarTodosAsync();
            var usuario = usuarios.FirstOrDefault(u => u.Email == login.Email);

            if (usuario == null || !usuario.Ativo)
            {
                return Unauthorized("E-mail ou senha inválidos.");
            }

            var senhaCorreta = _usuarioService.ConferirSenha(login.Senha, usuario.SenhaHash);
            if (!senhaCorreta)
            {
                return Unauthorized("E-mail ou senha inválidos.");
            }

            var token = _tokenService.GerarToken(usuario);

            var resposta = new LoginRespostaDTO
            {
                Token = token,
                Nome = usuario.Nome,
                Perfil = usuario.Perfil != null ? usuario.Perfil.Nome : ""
            };

            return Ok(resposta);
        }
    }
}