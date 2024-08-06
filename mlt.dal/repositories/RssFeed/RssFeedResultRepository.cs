using AutoMapper;
using Microsoft.Extensions.Options;
using mlt.common.domainEntities.RssFeed;
using mlt.dal.models.RssFeed;
using mlt.dal.Options;
using MongoDB.Driver;

namespace mlt.dal.repositories.RssFeed;

internal class RssFeedResultRepository : IRssFeedResultRepository
{
    private readonly IMapper _mapper;
    private readonly IMongoCollection<RssFeedResultModel> _rssFeedResults;

    public RssFeedResultRepository(IOptions<MongoDbOptions> settings, IMapper mapper)
    {
        _mapper = mapper;
        var client = new MongoClient(settings.Value.ConnectionString);
        _rssFeedResults = client.GetDatabase(settings.Value.RssLibraryDatabaseName).GetCollection<RssFeedResultModel>("RssFeedResults");
    }

    public async Task<IEnumerable<RssFeedResult>> GetAll()
        => _mapper.Map<IEnumerable<RssFeedResult>>((await _rssFeedResults.FindAsync(result => true)).ToList());

    public async Task<RssFeedResult> GetById(string id)
        => _mapper.Map<RssFeedResult>((await _rssFeedResults.FindAsync(result => result.Id == id)).FirstOrDefault());

    public Task Add(RssFeedResult result)
        => _rssFeedResults.InsertOneAsync(_mapper.Map<RssFeedResultModel>(result));

    public async Task Update(string id, RssFeedResult result)
    {
        result.Id = id;
        await _rssFeedResults.ReplaceOneAsync(existingResult => existingResult.Id == id, _mapper.Map<RssFeedResultModel>(result));
    }

    public Task Delete(string id)
        => _rssFeedResults.DeleteOneAsync(result => result.Id == id);
}