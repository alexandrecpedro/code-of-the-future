using Database.Interfaces;

namespace Negocio.Servicos;

public class Servico<T>
{
    private IRepositorio<T> _repositorio;

    public Servico(IRepositorio<T> repositorio)
    {
        _repositorio = repositorio;
    }

    public void Apagar(string id)
    {
        _repositorio.Apagar(id);
    }

    public T? BuscaPorId(string id)
    {
        return _repositorio.BuscaPorId(id);
    }

    public List<T> BuscarTodos(string criterio = "")
    {
        if (string.IsNullOrEmpty(criterio))
        {
            return _repositorio.BuscarTodos();
        }
        return _repositorio.BuscarTodos($"{criterio}");
    }

    public void Salvar(T obj)
    {
        _repositorio.Salvar(obj);
    }
}