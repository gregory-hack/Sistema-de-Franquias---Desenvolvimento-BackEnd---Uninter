
using System.ComponentModel.DataAnnotations;

namespace Franquias.Api.DTOs
{
    public class UsuarioCadastroDTO
    {
        [Required] public string Nome { get; set; } = string.Empty;
        [Required, EmailAddress] public string Email { get; set; } = string.Empty;
        [Required, MinLength(6)] public string Senha { get; set; } = string.Empty;
        [Range(1, int.MaxValue)] public int PerfilId { get; set; }
    }

    
}