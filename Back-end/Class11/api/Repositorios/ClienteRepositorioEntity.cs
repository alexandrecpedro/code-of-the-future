using api.Models;
using api.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace api.ModelViews;


public class ClienteContexto : DbContext 
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var conexao = Environment.GetEnvironmentVariable("DATABASE_URL_CDF");
        if(conexao is null) conexao = "Server=localhost;Database=locacaoVeiculosCDFGroupBy;Uid=root;Pwd=root;";
        optionsBuilder.UseMySql(conexao, ServerVersion.AutoDetect(conexao));
    }

    public DbSet<Cliente> Clientes { get; set; } = default!;
}

public class ClienteRepositorioEntity : IServico
{
    private ClienteContexto contexto;
    public ClienteRepositorioEntity()
    {
        contexto = new ClienteContexto();
    }

    private string? conexao = null;

    public async Task<List<Cliente>> TodosAsync()
    {
        return await contexto.Clientes.ToListAsync();
    }

    public async Task IncluirAsync(Cliente cliente)
    {
        contexto.Clientes.Add(cliente);
        await contexto.SaveChangesAsync();
    }

    public async Task<Cliente> AtualizarAsync(Cliente cliente)
    {
        contexto.Entry(cliente).State = EntityState.Modified;
        await contexto.SaveChangesAsync();

        return cliente;
    }

    public async Task ApagarAsync(Cliente cliente)
    {
        var obj = await contexto.Clientes.FindAsync(cliente.Id);
        if(obj is null) throw new Exception("Cliente não encontrado");
        contexto.Clientes.Remove(obj);
        await contexto.SaveChangesAsync();
    }
}