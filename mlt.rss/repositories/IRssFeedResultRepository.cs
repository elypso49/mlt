﻿namespace mlt.rss.repositories;

internal interface IRssFeedResultRepository : ICrudRepository<RssFeedResult>
{
    Task<IEnumerable<RssFeedResult>> GetByRssFeedId(string rssFeedId);
    Task<IEnumerable<RssFeedResult>> GetByStatus(StateValue stateValue);
}