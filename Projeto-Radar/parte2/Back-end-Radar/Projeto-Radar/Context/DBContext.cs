using Microsoft.EntityFrameworkCore;
using Projeto_Radar.Entitys;

namespace Projeto_Radar.Context
{
    public class DBContext : DbContext
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options) { }


        public DbSet<Categoria> Categorias { get; set; } = default!;

        public DbSet<Produto> Produtos { get; set; } = default!;

        public DbSet<Cliente> Clientes { get; set; } = default!;

        public DbSet<Pedido> Pedidos { get; set; } = default!;

        public DbSet<PedidoProduto> PedidoProdutos { get; set; } = default!;

        public DbSet<Loja> Lojas { get; set; } = default!;

        public DbSet<Campanha> Campanhas { get; set; } = default!;

        public DbSet<PosicoesProduto> PosicoesProdutos { get; set; } = default!;

        public DbSet<Usuario> Usuarios { get; set; } = default!;

    }
}
