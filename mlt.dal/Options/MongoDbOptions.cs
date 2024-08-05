using Microsoft.Extensions.Options;

namespace mlt.dal.dbSettings;

public class MongoDbOptions : IOptions<MongoDbOptions>
{
    public string ConnectionString { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;

    public MongoDbOptions Value => this;
}