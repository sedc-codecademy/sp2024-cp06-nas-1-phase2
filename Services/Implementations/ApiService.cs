using Services.Interfaces;
using DTOs.Article;
using Services.Helpers;

namespace Services.Implementations
{
    public class ApiService : IApiService
    {
        private readonly IArticleService _articleService;

        public ApiService(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public async Task<PaginatedResult<ArticleDto>> GetArticlesAsync(int pageNumber, int pageSize)
        {

            return await _articleService.GetPagedArticlesAsync(pageNumber, pageSize);
        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesBySourceAsync(int rssFeedId)
        {
            return await _articleService.GetAllArticlesBySourceAsync(rssFeedId);
        }
        public async Task<IEnumerable<ArticleDto>> GetPagedArticlesBySourceAsync(int rssFeedId, int pageNumber, int pageSize)
        {
            return await _articleService.GetPagedArticlesBySourceAsync(rssFeedId, pageNumber, pageSize);
        }
    }
}
