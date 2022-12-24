using Business.Entidades;
// using Database.Interfaces;
// using Database.MySql.Interfaces;
using Database.MySQL.Repositorios;

namespace Business.Servicos;

public class ClienteServico
{
    // private IRepositorio<Cliente> _repositorio;
    // private IRepositorioSql<Cliente> _repositorioSql;
    private RepositorioMySQL<Cliente> _repositorio;

    // public ClienteServico(IRepositorio<Cliente> repositorio, IRepositorioSql<Cliente> repositorioSql)
    // {
    //     _repositorio = repositorio;
    //     _repositorioSql = repositorioSql;
    // }

    public ClienteServico(RepositorioMySQL<Cliente> repositorio)
    {
        _repositorio = repositorio;
    }

    public void Salvar(Cliente cliente)
    {
        _repositorio.Salvar(cliente);
    }

    public List<Cliente> BuscaPorIdOuEmail(string idOuEmail)
    {
        // return _repositorioSql.BuscaPorIdOuEmail(idOuEmail);
        return _repositorio.BuscaPorIdOuEmail(idOuEmail);
    }

    public List<Cliente> Todos()
    {
        return _repositorio.Todos();
    }

    public void ApagaPorId(int id)
    {
        // _repositorioSql.ApagaPorId(id);
        _repositorio.ApagaPorId(id);
    }

    public Cliente? BuscaPorId(int id)
    {
        // return _repositorioSql.BuscaPorId(id);
        return _repositorio.BuscaPorId(id);
    }
}