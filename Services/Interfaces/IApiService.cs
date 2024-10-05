using DTOs.Article;

namespace Services.Interfaces
{
    public interface IApiService
    {
        Task<IEnumerable<ArticleDto>> GetArticlesAsync();
        Task<IEnumerable<ArticleDto>> GetArticlesBySourceAsync(int rssFeedId);
    }
}
