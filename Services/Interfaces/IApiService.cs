using DTOs.Article;
using Services.Helpers;

namespace Services.Interfaces
{
    public interface IApiService
    {
        Task<PaginatedResult<ArticleDto>> GetArticlesAsync(int pageNumber, int pageSize);
        Task<IEnumerable<ArticleDto>> GetArticlesBySourceAsync(int rssFeedId);
        Task<IEnumerable<ArticleDto>> GetPagedArticlesBySourceAsync(int rssFeedId, int pageNumber, int pageSize);

    }
}
