
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Franquias.Api.Models;

namespace Franquias.Api.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuracao;

        public TokenService(IConfiguration configuracao)
        {
            _configuracao = configuracao;
        }

        public string GerarToken(Usuario usuario)
        {
            var chave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuracao["Jwt:Chave"]!));
            var credenciais = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.Role, usuario.Perfil != null ? usuario.Perfil.Nome : "")
            };

            var token = new JwtSecurityToken(
                issuer: _configuracao["Jwt:Emissor"],
                audience: _configuracao["Jwt:Audiencia"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credenciais
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}