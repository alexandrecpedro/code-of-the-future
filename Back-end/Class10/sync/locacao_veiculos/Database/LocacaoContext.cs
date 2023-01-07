using locacao_veiculos.Models;
using Microsoft.EntityFrameworkCore;

namespace locacao_veiculos.Database;

public class LocacaoContext: DbContext 
{
    public LocacaoContext( DbContextOptions<LocacaoContext> options ): base(options) { }

    public DbSet<Carro> Carros { get; set; } = default!;
    public DbSet<Modelo> Modelos { get; set; } = default!;
    public DbSet<Marca> Marcas { get; set; } = default!;
    public DbSet<Pedido> Pedidos { get; set; } = default!;
    public DbSet<Cliente> Clientes { get; set; } = default!;
    public DbSet<Configuracao> Configuracoes { get; set; } = default!;
}
