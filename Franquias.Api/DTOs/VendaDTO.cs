
namespace Franquias.Api.DTOs
{
    public class VendaDTO
    {
        [System.ComponentModel.DataAnnotations.RangeAttribute
        (1, int.MaxValue, ErrorMessage = "A unidade franqueada deve ser informada.")]
        public int UnidadeFranqueadaId { get; set; }

        [System.ComponentModel.DataAnnotations.RangeAttribute
        (1, int.MaxValue, ErrorMessage = "O usuário deve ser informado.")]
        public int UsuarioId { get; set; }
        
        [System.ComponentModel.DataAnnotations.MinLengthAttribute
        (1, ErrorMessage = "A venda deve possuir pelo menos um item.")]
        public List<ItemVendaDTO> Itens { get; set; } = new List<ItemVendaDTO>();
    }
}