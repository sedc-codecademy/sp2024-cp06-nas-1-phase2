using DomainModels;
using DTOs.RssFeed;

namespace Mappers
{
    public static class RssSourceMappers
    {
        public static RssFeed ToRssSourceModel(this RssFeedDto rssFeedDto)
        {
            return new RssFeed
            {
                Source = rssFeedDto.Source,
                SourceUrl = rssFeedDto.SourceUrl,
                FeedUrl = rssFeedDto.FeedUrl,
                Title = rssFeedDto.Title,
                Description = rssFeedDto.Description,
                Link = rssFeedDto.Link,
                Author = rssFeedDto.Author,
                PubDate = rssFeedDto.PubDate,
                //UrlToImageList = rssFeedDto.UrlToImageList,
                //UrlToImage = rssFeedDto.UrlToImage,
            };
        }
        public static RssFeedDto ToRssSourceDto(this RssFeed rssFeedDto)
        {
            return new RssFeedDto
            {
                Source = rssFeedDto.Source,
                SourceUrl = rssFeedDto.SourceUrl,
                FeedUrl = rssFeedDto.FeedUrl,
                Title = rssFeedDto.Title,
                Description = rssFeedDto.Description,
                Link = rssFeedDto.Link,
                Author = rssFeedDto.Author,
                PubDate = rssFeedDto.PubDate,
                //UrlToImageList = rssFeedDto.UrlToImageList,
                //UrlToImage = rssFeedDto.UrlToImage,
            };
        }
    }
}
