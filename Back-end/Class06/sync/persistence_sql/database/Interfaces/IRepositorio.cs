namespace Database.Interfaces;
public interface IRepositorio<T>
{
    void Salvar(T obj);
    List<T> BuscarTodos(string criterio = "");
    T? BuscaPorId(string id);
    void Apagar(string id);
}