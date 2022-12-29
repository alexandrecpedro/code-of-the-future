using Database.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Database.MongoDb.Repositorios;

public class RepositorioMongoDb<T> : IRepositorio<T>, IRepositorioMongoDb<T>
{
    // exemplo de implementação driver mongo
    // https://github.com/torneseumprogramador/api-desafio21dias-pais/blob/main/Servico/PaisMongodb.cs

    private static readonly string? connection = Environment.GetEnvironmentVariable("DATABASE_CODE_OF_THE_FUTURE_MONGODB")!;

    public IMongoQueryable<T> BuscaCriterio()
    {
        return documento<T>().AsQueryable();
    }

    public void Excluir<T>(ObjectId id)
    {
        documento<T>().DeleteOne(doc => ((IDoc)doc!).Id == id);
    }

    private IMongoCollection<T> documento<T>()
    {
        IMongoClient client = new MongoClient(connection);
        var nomeDoBanco = connection?.ToString().Split('/').Last();
        IMongoDatabase database = client.GetDatabase(nomeDoBanco);
        return database.GetCollection<T>((typeof(T)).Name);
    }

    public void RemoverColecao<T>()
    {
        documento<T>().Database.DropCollection(typeof(T).Name);
    }

    public void Salvar(T obj)
    {
        if (string.IsNullOrEmpty(obj?.ToString()))
            throw new NullReferenceException();

        var item = (IDoc)obj;

        if (item.Id == ObjectId.Parse("000000000000000000000000"))
        {
            documento<T>().InsertOne(obj);
        }
        else
        {
            foreach (var prop in obj.GetType().GetProperties())
            {
                var valor = item?.GetType()?.GetProperty(prop.Name)?.GetValue(item);
                if (string.IsNullOrEmpty(valor?.ToString()))
                {
                    var update = Builders<T>.Update.Set(prop.Name, valor);
                    var filter = Builders<T>.Filter.Eq("_id", item?.Id);
                    documento<T>().UpdateOne(filter, update);
                }
            }
        }
    }

    public List<T> Todos(string criterio = "")
    {
        return BuscaCriterio().ToList();
    }
}