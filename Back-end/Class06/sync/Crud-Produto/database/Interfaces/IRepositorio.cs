namespace database.interfaces;
public interface IRepositorio<T>
{
    void Salvar(T obj);
    List<T> BuscarTodos(string criterio="");
    T? BuscaPorId(int id);
    void ApagarPorId(int id);
}