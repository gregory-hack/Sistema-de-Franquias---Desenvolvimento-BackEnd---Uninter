
using Franquias.Api.Models;

namespace Franquias.Api.DTOs
{
    public class ProdutoServicoDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public decimal PrecoBase { get; set; }
        public int CategoriaId { get; set; }
        public int? FornecedorId { get; set; }
    }

}
 
