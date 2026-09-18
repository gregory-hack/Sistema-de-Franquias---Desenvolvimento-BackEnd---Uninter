
namespace Franquias.Api.DTOs
{
    public class FranqueadoraDTO
    {
        public string NomeFantasia { get; set; } = string.Empty;
        public string RazaoSocial { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public DateTime DataFundacao { get; set; }
    }
}