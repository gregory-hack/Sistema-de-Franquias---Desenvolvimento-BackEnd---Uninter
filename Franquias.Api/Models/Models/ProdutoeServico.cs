
namespace Franquias.Api.Models
{
    public class ProdutoServico
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal PrecoBase { get; set; }
        public string Status { get; set; } = "Ativo";

        public int CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }
        public int? FornecedorId { get; set; }
        public Fornecedor? Fornecedor { get; set; }
    }
 
    
}

