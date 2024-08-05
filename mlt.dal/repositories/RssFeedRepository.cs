using AutoMapper;
using Microsoft.Extensions.Options;
using mlt.common.domainEntities;
using mlt.dal.dbSettings;
using mlt.dal.models;
using MongoDB.Driver;

namespace mlt.dal.repositories;

internal class RssFeedRepository : IRssFeedRepository
{
    private readonly IMongoCollection<RssFeedModel> _rssFeeds;
    private readonly IMapper _mapper;

    public RssFeedRepository(IOptions<MongoDbOptions> settings, IMapper mapper)
    {
        _mapper = mapper;
        var client = new MongoClient(settings.Value.ConnectionString);
        _rssFeeds = client.GetDatabase(settings.Value.DatabaseName).GetCollection<RssFeedModel>("RssFeeds");
    }

    public async Task<IEnumerable<RssFeed>> GetAll()
        => _mapper.Map<IEnumerable<RssFeed>>((await _rssFeeds.FindAsync(feed => true)).ToList());

    public async Task<RssFeed> GetById(string id)
        => _mapper.Map<RssFeed>((await _rssFeeds.FindAsync(feed => feed.Id == id)).ToList().FirstOrDefault());

    public async Task<RssFeed> Add(RssFeed feed)
    {
        var model = _mapper.Map<RssFeedModel>(feed);
        await _rssFeeds.InsertOneAsync(model);
        feed = _mapper.Map<RssFeed>(model);

        return feed;
    }

    public async Task<UpdateResult> Update(string id, RssFeed feed)
        => _mapper.Map<UpdateResult>(await _rssFeeds.ReplaceOneAsync(existingFeed => existingFeed.Id == id, _mapper.Map<RssFeedModel>(feed))).ValidateResult();

    public async Task<DeleteResult> Delete(string id)
        => _mapper.Map<DeleteResult>(await _rssFeeds.DeleteOneAsync(feed => feed.Id == id)).ValidateResult();
}