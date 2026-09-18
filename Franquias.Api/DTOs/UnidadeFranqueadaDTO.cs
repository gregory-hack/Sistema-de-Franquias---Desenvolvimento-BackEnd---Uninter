
namespace Franquias.Api.DTOs
{
    public class UnidadeFranqueadaDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public int FranqueadoraId { get; set; }
    }
}