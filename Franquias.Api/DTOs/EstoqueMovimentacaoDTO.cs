
using System.ComponentModel.DataAnnotations;

namespace Franquias.Api.DTOs
{
    public class EstoqueMovimentacaoDTO
    {
        public int UnidadeFranqueadaId { get; set; }
        public int ProdutoServicoId { get; set; }

        [System.ComponentModel.DataAnnotations.RangeAttribute
        
        (1, int.MaxValue, ErrorMessage = "A quantidade deve ser maior que zero.")]
        public int Quantidade { get; set; }
        

    }
       
       
}