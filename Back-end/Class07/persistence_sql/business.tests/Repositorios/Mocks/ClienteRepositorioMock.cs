using Business.Entidades;
using Database.Interfaces;
using Database.MySql.Interfaces;

namespace business.tests.Repositorios.Mocks;

public class ClienteRepositorioMock : IRepositorio<Cliente>, IRepositorioSql<Cliente>
{
    private static List<Cliente> clientes = new List<Cliente>();

    public void Salvar(Cliente cliente)
    {
        clientes.Add(cliente);
    }

    public List<Cliente> BuscaPorIdOuEmail(string idOuEmail)
    {
        return new List<Cliente>();
    }

    public List<Cliente> Todos(string criterio = "")
    {
        return new List<Cliente>()
        {
            new Cliente()
        };
    }

    public void ApagaPorId(int id)
    {
        clientes.RemoveAt(id);
    }

    public Cliente? BuscaPorId(int id)
    {
        return new Cliente();
    }
}