using Business.Entidades;
// using Database.Interfaces;
// using Database.MySql.Interfaces;
using Database.MySQL.Repositorios;

namespace Business.Servicos;

public class FornecedorServico
{
    // private IRepositorio<Fornecedor> _repositorio;
    // private IRepositorioSql<Fornecedor> _repositorioSql;
    private RepositorioMySQL<Fornecedor> _repositorio;

    // public FornecedorServico(IRepositorio<Fornecedor> repositorio, IRepositorioSql<Fornecedor> repositorioSql)
    // {
    //     _repositorio = repositorio;
    //     _repositorioSql = repositorioSql;
    // }

    public FornecedorServico(RepositorioMySQL<Fornecedor> repositorio)
    {
        _repositorio = repositorio;
    }

    public void Salvar(Fornecedor fornecedor)
    {
        _repositorio.Salvar(fornecedor);
    }

    public List<Fornecedor> BuscaPorIdOuEmail(string idOuEmail)
    {
        // return _repositorioSql.BuscaPorIdOuEmail(idOuEmail);
        return _repositorio.BuscaPorIdOuEmail(idOuEmail);
    }

    public List<Fornecedor> Todos()
    {
        return _repositorio.Todos();
    }

    public void ApagaPorId(int id)
    {
        // _repositorioSql.ApagaPorId(id);
        _repositorio.ApagaPorId(id);
    }

    public Fornecedor? BuscaPorId(int id)
    {
        // return _repositorioSql.BuscaPorId(id);
        return _repositorio.BuscaPorId(id);
    }
}