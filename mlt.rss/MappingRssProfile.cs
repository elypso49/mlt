using mlt.common.datas.dtos;

namespace mlt.rss;

public class MappingRssProfile : Profile
{
    public MappingRssProfile()
    {
        CreateMap<RssFeedModel, RssFeed>().ReverseMap();
        CreateMap<RssFeedResultModel, RssFeedResult>().ReverseMap();
        CreateMap<DeleteResult.Acknowledged, DeleteResponse>();
        CreateMap<ReplaceOneResult.Acknowledged, UpdateResponse>();
    }
}