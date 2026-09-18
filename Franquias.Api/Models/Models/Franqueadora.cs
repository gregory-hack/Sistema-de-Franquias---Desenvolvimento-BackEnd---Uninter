
namespace Franquias.Api.Models
{
    public class Franqueadora
    {
        public int Id { get; set; }
        public string NomeFantasia { get; set; } = string.Empty;
        public string RazaoSocial { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public DateTime DataFundacao { get; set; }

        public string Status {get ; set;} = "Ativo";

        public ICollection<UnidadeFranqueada> Unidades { get; set; } = new List<UnidadeFranqueada>();
    }
}