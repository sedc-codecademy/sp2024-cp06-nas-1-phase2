using AutoMapper;
using DomainModels;
using DTOs.Article;
using DTOs.RssFeed;
using DTOs.UrlToImageConfig;

namespace Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //RssFeed
            CreateMap<RssFeed, RssFeedDto>().ReverseMap();
            CreateMap<RssFeed, AddRssFeedDto>().ReverseMap();
            CreateMap<RssFeed, UpdateRssFeedDto>().ReverseMap();

            //Article
            CreateMap<Article, ArticleDto>().ReverseMap();

            //UrlToImageConfig
            CreateMap<UrlToImageConfig, UrlToImageConfigDto>().ReverseMap();
        }
    }
}
