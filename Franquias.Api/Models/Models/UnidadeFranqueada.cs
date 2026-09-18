
namespace Franquias.Api.Models
{
    public class UnidadeFranqueada
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string Endereco { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public DateTime DataInicio { get; set; }
        public string Situacao { get; set; } = "Ativa";

        public int FranqueadoraId { get; set; }
        public Franqueadora? Franqueadora { get; set; }
    }
}   