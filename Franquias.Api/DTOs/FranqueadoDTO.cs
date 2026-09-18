
namespace Franquias.Api.DTOs
{
    public class FranqueadoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public int UnidadeFranqueadaId { get; set; }
    }
}