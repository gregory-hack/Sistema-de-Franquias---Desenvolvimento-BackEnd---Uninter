
namespace Franquias.Api.Models
{
    public class Franqueado
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;

        public int UnidadeFranqueadaId { get; set; }
        public UnidadeFranqueada? UnidadeFranqueada { get; set; }
    }
}