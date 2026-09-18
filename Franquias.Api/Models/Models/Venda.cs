
namespace Franquias.Api.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.Now;
        public decimal ValorTotal { get; set; }

        public int UnidadeFranqueadaId { get; set; }
        public UnidadeFranqueada? UnidadeFranqueada { get; set; }

        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public ICollection<ItemVenda> Itens { get; set; } = new List<ItemVenda>();
    }
}