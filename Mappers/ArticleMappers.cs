using DomainModels;
using DTOs;

namespace Mappers
{
    public static class ArticleMappers
    {
        public static Article ToArticleModel(this ArticleDto articleDto)
        {
            return new Article
            {
                Source = articleDto.Source,
                SourceUrl = articleDto.SourceUrl,
                FeedUrl = articleDto.FeedUrl,
                Title = articleDto.Title,
                Description = articleDto.Description,
                Link = articleDto.Link,
                Author = articleDto.Author,
                PubDate = articleDto.PubDate,
                UrlToImage = articleDto.UrlToImage,
                TrustScore = articleDto.TrustScore
            };
        }

        public static ArticleDto ToArticleDto(this Article articleDto)
        {
            return new ArticleDto
            {
                Source = articleDto.Source,
                SourceUrl = articleDto.SourceUrl,
                FeedUrl = articleDto.FeedUrl,
                Title = articleDto.Title,
                Description = articleDto.Description,
                Link = articleDto.Link,
                Author = articleDto.Author,
                PubDate = articleDto.PubDate,
                UrlToImage = articleDto.UrlToImage,
                TrustScore = articleDto.TrustScore
            };
        }
    }
}
