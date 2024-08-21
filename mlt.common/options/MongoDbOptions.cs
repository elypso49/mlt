using Microsoft.Extensions.Options;

namespace mlt.common.options;

public class MongoDbOptions : IOptions<MongoDbOptions>
{
    public string ConnectionString { get; init; } = null!;
    public string RssLibraryDatabaseName { get; init; } = null!;

    public MongoDbOptions Value => this;
}