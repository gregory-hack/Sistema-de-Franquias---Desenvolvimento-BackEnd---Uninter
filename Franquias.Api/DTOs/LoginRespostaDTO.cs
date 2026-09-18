
namespace Franquias.Api.DTOs
{
    public class LoginRespostaDTO
    {
        public string Token { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Perfil { get; set; } = string.Empty;
    }
}