using api.Models;
using api.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.Repositorios.Entity;

public class ContextoEntity : DbContext 
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conexao = Environment.GetEnvironmentVariable("DATABASE_URL_CDF");
        if(conexao is null) conexao = "Server=localhost;Database=locacaoVeiculosCDFGroupBy;Uid=root;Pwd=root;";
        optionsBuilder.UseMySql(conexao, ServerVersion.AutoDetect(conexao));
    }

    public DbSet<Administrador> Administradores { get; set; } = default!;
    public DbSet<Cliente> Clientes { get; set; } = default!;
}