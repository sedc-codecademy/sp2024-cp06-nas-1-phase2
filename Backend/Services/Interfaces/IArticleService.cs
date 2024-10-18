using DomainModels;
using DTOs.Article;
using Services.Helpers;
using System.Xml.Linq;

namespace Services.Interfaces
{
    public interface IArticleService
    {
        //public List<Article> CreateArticles(List<XElement> items, RssFeed urlConfig);

        //Task<IEnumerable<ArticleDto>> GetPagedArticlesAsync(int pageNumber, int pageSize);
        Task<PaginatedResult<ArticleDto>> GetPagedArticlesAsync(int pageNumber, int pageSize);
        Task<IEnumerable<ArticleDto>> GetAllArticlesBySourceAsync(int rssFeedId);
        Task AddArticlesAsync(IEnumerable<ArticleDto> addArticles, CancellationToken cancellationToken);
        Task<IEnumerable<ArticleDto>> GetPagedArticlesBySourceAsync(int rssFeedId, int pageNumber, int pageSize);
    }
}
