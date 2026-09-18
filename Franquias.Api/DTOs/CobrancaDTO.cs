
namespace Franquias.Api.DTOs
{
    public class CobrancaDTO
    {
        public int UnidadeFranqueadaId { get; set; }
        public decimal Percentual { get; set; }
        public DateTime InicioPeriodo { get; set; }
        public DateTime FimPeriodo { get; set; }
        public string PeriodoTexto { get; set; } = string.Empty;
    }
}