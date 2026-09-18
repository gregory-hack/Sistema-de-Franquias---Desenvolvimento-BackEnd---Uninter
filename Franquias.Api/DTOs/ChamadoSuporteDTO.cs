
namespace Franquias.Api.DTOs
{
    public class ChamadoSuporteDTO
    {
        public int UnidadeFranqueadaId { get; set; }
        public int UsuarioId { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Prioridade { get; set; } = "Media";
        public string Descricao { get; set; } = string.Empty;
    }
}