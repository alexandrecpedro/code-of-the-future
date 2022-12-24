namespace Database.Interfaces;

public interface IRepositorio<T>
{
    void Salvar(T obj);

    List<T> Todos(string criterio = "");
}