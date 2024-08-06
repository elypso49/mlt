using AutoMapper;
using Microsoft.Extensions.Options;
using mlt.dal.models.RssFeed;
using mlt.dal.Options;
using MongoDB.Driver;
using DeleteResult = mlt.dal.resultDtos.DeleteResult;
using UpdateResult = mlt.dal.resultDtos.UpdateResult;

namespace mlt.dal.repositories.RssFeed;

internal class RssFeedRepository : IRssFeedRepository
{
    private readonly IMongoCollection<RssFeedModel> _rssFeeds;
    private readonly IMapper _mapper;

    public RssFeedRepository(IOptions<MongoDbOptions> settings, IMapper mapper)
    {
        _mapper = mapper;
        var client = new MongoClient(settings.Value.ConnectionString);
        _rssFeeds = client.GetDatabase(settings.Value.RssLibraryDatabaseName).GetCollection<RssFeedModel>("RssFeeds");
    }

    public async Task<IEnumerable<common.domainEntities.RssFeed.RssFeed>> GetAll()
        => _mapper.Map<IEnumerable<common.domainEntities.RssFeed.RssFeed>>((await _rssFeeds.FindAsync(feed => true)).ToList());

    public async Task<common.domainEntities.RssFeed.RssFeed> GetById(string id)
        => _mapper.Map<common.domainEntities.RssFeed.RssFeed>((await _rssFeeds.FindAsync(feed => feed.Id == id)).ToList().FirstOrDefault());

    public async Task<common.domainEntities.RssFeed.RssFeed> Add(common.domainEntities.RssFeed.RssFeed feed)
    {
        var model = _mapper.Map<RssFeedModel>(feed);
        await _rssFeeds.InsertOneAsync(model);
        feed = _mapper.Map<common.domainEntities.RssFeed.RssFeed>(model);

        return feed;
    }

    public async Task<UpdateResult> Update(string id, common.domainEntities.RssFeed.RssFeed feed)
        => _mapper.Map<UpdateResult>(await _rssFeeds.ReplaceOneAsync(existingFeed => existingFeed.Id == id, _mapper.Map<RssFeedModel>(feed))).ValidateResult();

    public async Task<DeleteResult> Delete(string id)
        => _mapper.Map<DeleteResult>(await _rssFeeds.DeleteOneAsync(feed => feed.Id == id)).ValidateResult();
}