
namespace Franquias.Api.Models
{
    public class ChamadoSuporte
    {
        public int Id { get; set; }
        public string Categoria { get; set; } = string.Empty;
        public string Prioridade { get; set; } = "Media";
        public string Descricao { get; set; } = string.Empty;
        public string Status { get; set; } = "Aberto";
        public DateTime DataAbertura { get; set; } = DateTime.Now;
        public DateTime? DataEncerramento { get; set; }

        public int UnidadeFranqueadaId { get; set; }
        public UnidadeFranqueada? UnidadeFranqueada { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }
    }
}
