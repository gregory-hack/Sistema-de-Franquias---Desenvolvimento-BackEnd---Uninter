
namespace Franquias.Api.Models
{
    public class Cobranca
    {
        public int Id { get; set; }
        public decimal Percentual { get; set; }
        public string Periodo { get; set; } = string.Empty;
        public decimal Faturamento { get; set; }
        public decimal ValorCobranca { get; set; }
        public string StatusPagamento { get; set; } = "Pendente";

        public int UnidadeFranqueadaId { get; set; }
        public UnidadeFranqueada? UnidadeFranqueada { get; set; }
    }
}