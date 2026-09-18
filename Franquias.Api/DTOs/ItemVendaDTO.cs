
namespace Franquias.Api.DTOs
{
    public class ItemVendaDTO
    {
        public int ProdutoServicoId { get; set; }

       [System.ComponentModel.DataAnnotations.RangeAttribute(
        1,
        int. MaxValue,
        ErrorMessage = "A quantidade deve ser maior que zer"
       )]
        public int Quantidade { get; set; }
        
    }
}