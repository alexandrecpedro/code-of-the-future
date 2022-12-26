namespace Database.Interfaces;

public interface IRepositorioSql<T>
{
    List<T> BuscaPorIdOuEmail(string idOuEmail);

    void ApagaPorId(int id);

    T? BuscaPorId(int id);
}