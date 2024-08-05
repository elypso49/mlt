using Microsoft.Extensions.Options;

namespace mlt.dal.Options;

public class MongoDbOptions : IOptions<MongoDbOptions>
{
    public string ConnectionString { get; set; } = null!;
    public string RssLibraryDatabaseName { get; set; } = null!;

    public MongoDbOptions Value => this;
}