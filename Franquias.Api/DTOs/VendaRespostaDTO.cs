

namespace Franquias.Api.DTOs
{
    public class VendaRespostaDTO
{
    public int Id { get; set; }
    public int UnidadeFranqueadaId { get; set; }
    public int UsuarioId { get; set; }
    public DateTime DataVenda { get; set; }
    public decimal ValorTotal { get; set; }
    public List<ItemVendaRespostaDTO> Itens { get; set; } = [];
}

public class ItemVendaRespostaDTO
{
    public int Id { get; set; }
    public int ProdutoServicoId { get; set; }
    public int Quantidade { get; set; }
    public decimal PrecoUnitario { get; set; }
    public decimal Subtotal { get; set; }
}
    
}
