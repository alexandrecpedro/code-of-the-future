using api.Models;

namespace api.Repositorios.Interfaces;

public interface IServico<T>
{
    Task<List<T>> TodosAsync();
    Task IncluirAsync(T obj);
    Task<T> AtualizarAsync(T obj);
    Task ApagarAsync(T obj);
}