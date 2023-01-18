using locacao_veiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace locacao_veiculos.Database;

public class LocacaoContext: DbContext 
{
    public LocacaoContext() { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conn = Environment.GetEnvironmentVariable("DATABASE_CF_URL_TEST");
        if(conn is not null) optionsBuilder.UseMySql(conn, ServerVersion.AutoDetect(conn));
    }


    public LocacaoContext( DbContextOptions<LocacaoContext> options ): base(options) { }

    public DbSet<Carro> Carros { get; set; } = default!;
    public DbSet<Modelo> Modelos { get; set; } = default!;
    public DbSet<Marca> Marcas { get; set; } = default!;
    public DbSet<Pedido> Pedidos { get; set; } = default!;
    public DbSet<Cliente> Clientes { get; set; } = default!;
    public DbSet<Configuracao> Configuracoes { get; set; } = default!;
    public DbSet<PedidoCarro> PedidoCarros { get; set; } = default!;
}
