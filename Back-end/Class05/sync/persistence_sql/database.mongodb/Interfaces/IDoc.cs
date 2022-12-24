using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Database.MongoDb.Interfaces;

public interface IDoc
{
    [BsonId()]
    public ObjectId Id { get; set; }
}