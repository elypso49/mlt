using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mlt.common;

public abstract class BsonIdentifiable
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
}