using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mlt.dal.models;

public class RssFeedModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public DateTime LastUpdate { get; set; }
    public string Category { get; set; } = null!;
    public string DestinationFolder { get; set; } = null!;
}