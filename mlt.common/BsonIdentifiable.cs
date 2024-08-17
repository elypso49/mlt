namespace mlt.common;

public abstract class BsonIdentifiable
{
    [BsonId, BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
}