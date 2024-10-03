using DomainModels;
using DTOs.Article;
using System.Xml.Linq;

namespace Services.Interfaces
{
    public interface IArticleService
    {
        //public List<Article> CreateArticles(List<XElement> items, RssFeed urlConfig);

        Task AddArticlesAsync(IEnumerable<ArticleDto> articles);
        Task<IEnumerable<ArticleDto>> GetAllArticlesAsync();
        Task<IEnumerable<ArticleDto>> GetAllArticlesBySourceAsync(int rssFeedId);
    }
}
