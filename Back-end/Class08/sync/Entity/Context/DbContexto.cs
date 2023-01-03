using Entity.Models;
using Microsoft.EntityFrameworkCore;

namespace Entity.Context;

public class DbContexto : DbContext
{
    public DbContexto(DbContextOptions<DbContexto> options) : base(options) { }
    // public DbContexto() { }

    // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    // {
    //     var connection = Environment.GetEnvironmentVariable("DATABASE_CODE_OF_THE_FUTURE");
    //     if (connection is null) connection = "Server=localhost;Database=persistence_code_of_the_future;Uid=root;Pwd=root;";
    //     optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection));
    // }

    public DbSet<Cliente> Clientes { get; set; } = default!;
    public DbSet<Fornecedor> Fornecedores { get; set; } = default!;
}