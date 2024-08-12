using AutoMapper;
using mlt.common.datas.dtos;
using mlt.rss.datas.models;
using mlt.rss.dtos;

namespace mlt.rss;

public class MappingRssProfile : Profile
{
    public MappingRssProfile()
    {
        CreateMap<RssFeedModel, RssFeed>().ReverseMap();
        CreateMap<RssFeedResultModel, RssFeedResult>().ReverseMap();
        CreateMap<MongoDB.Driver.DeleteResult, DeleteResult>().ReverseMap();
    }
}