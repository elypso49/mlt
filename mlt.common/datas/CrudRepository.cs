namespace mlt.common.datas;

public abstract class CrudRepository<TEntity, TModel> : ICrudRepository<TEntity>
    where TEntity : class
    where TModel : BsonIdentifiable
{
    protected readonly IMongoCollection<TModel> Collection;
    protected readonly IMapper Mapper;

    protected CrudRepository(IOptions<MongoDbOptions> settings, IMapper mapper, string dataBaseName)
    {
        Mapper = mapper;
        var client = new MongoClient(settings.Value.ConnectionString);
        Collection = client.GetDatabase(settings.Value.RssLibraryDatabaseName).GetCollection<TModel>(dataBaseName);
    }

    public async Task<IEnumerable<TEntity>> GetAll() => Mapper.Map<IEnumerable<TEntity>>((await Collection.FindAsync(feed => true)).ToList());

    public async Task<TEntity> GetById(string id) => Mapper.Map<TEntity>((await Collection.FindAsync(x => x.Id == id)).ToList().FirstOrDefault());

    public async Task<TEntity> Add(TEntity entity)
    {
        var model = Mapper.Map<TModel>(entity);
        await Collection.InsertOneAsync(model);
        entity = Mapper.Map<TEntity>(model);

        return entity;
    }

    public async Task<UpdateResult> Update(string id, TEntity entity)
        => Mapper.Map<UpdateResult>(await Collection.ReplaceOneAsync(x => x.Id == id, Mapper.Map<TModel>(entity))).ValidateResult();

    public async Task<DeleteResult> Delete(string id) => Mapper.Map<DeleteResult>(await Collection.DeleteOneAsync(x => x.Id == id)).ValidateResult();
}