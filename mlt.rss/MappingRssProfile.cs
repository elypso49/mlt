namespace mlt.rss;

public class MappingRssProfile : Profile
{
    public MappingRssProfile()
    {
        CreateMap<RssFeedModel, RssFeed>().ReverseMap();
        CreateMap<RssFeedResultModel, RssFeedResult>().ReverseMap();
        CreateMap<DeleteResult, common.datas.dtos.DeleteResponse>().ReverseMap();
    }
}