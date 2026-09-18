
using Microsoft.EntityFrameworkCore;
using Franquias.Api.Models;

namespace Franquias.Api.Data
{
    public class DBFranquias : DbContext
    {
        public DBFranquias(DbContextOptions<DBFranquias> options) : base(options)
        {
        }

        public DbSet<Perfil> Perfis { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Franqueadora> Franqueadoras { get; set; }
        public DbSet<UnidadeFranqueada> UnidadesFranqueadas { get; set; }
        public DbSet<Franqueado> Franqueados { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<ProdutoServico> ProdutosServicos { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Estoque> Estoques { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<ItemVenda> ItensVenda { get; set; }
        public DbSet<Cobranca> Cobrancas { get; set; }
        public DbSet<ChamadoSuporte> ChamadosSuporte { get; set; }
    }
}