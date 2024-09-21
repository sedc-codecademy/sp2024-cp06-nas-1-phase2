using DomainModels;
using DTOs;

namespace Mappers
{
    public static class RssSourceMappers
    {
        public static RssSource ToRssSourceModel(this RssSourceDto rssSourceDto)
        {
            return new RssSource
            {
                Source = rssSourceDto.Source,
                SourceUrl = rssSourceDto.SourceUrl,
                FeedUrl = rssSourceDto.FeedUrl,
                Title = rssSourceDto.Title,
                Description = rssSourceDto.Description,
                Link = rssSourceDto.Link,
                Author = rssSourceDto.Author,
                PubDate = rssSourceDto.PubDate,
                //UrlToImageList = rssSourceDto.UrlToImageList,
                UrlToImage = rssSourceDto.UrlToImage,
            };
        }
        public static RssSourceDto ToRssSourceDto(this RssSource rssSourceDto)
        {
            return new RssSourceDto
            {
                Source = rssSourceDto.Source,
                SourceUrl = rssSourceDto.SourceUrl,
                FeedUrl = rssSourceDto.FeedUrl,
                Title = rssSourceDto.Title,
                Description = rssSourceDto.Description,
                Link = rssSourceDto.Link,
                Author = rssSourceDto.Author,
                PubDate = rssSourceDto.PubDate,
                //UrlToImageList = rssSourceDto.UrlToImageList,
                UrlToImage = rssSourceDto.UrlToImage,
            };
        }
    }
}
