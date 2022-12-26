using MongoDB.Bson;
using MongoDB.Driver.Linq;

namespace Database.Interfaces;

public interface IRepositorioMongoDb<T>
{
    void Excluir<T>(ObjectId id);
    void RemoverColecao<T>();
    IMongoQueryable<T> BuscaCriterio();
}