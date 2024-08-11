using mlt.dal.repositories.RssFeed;

namespace mlt.services.RssFeed;

public class RssFeedService(IRssFeedRepository rssFeedRepository) : CrudService<common.domainEntities.RssFeed>(rssFeedRepository), IRssFeedService { }