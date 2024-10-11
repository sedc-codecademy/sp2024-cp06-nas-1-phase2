using Services.Interfaces;
using DTOs.Article;

namespace Services.Implementations
{
    public class ApiService : IApiService
    {
        private readonly IArticleService _articleService;

        public ApiService(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public async Task<IEnumerable<ArticleDto>> GetArticlesAsync()
        {
            return await _articleService.GetAllArticlesAsync();
        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesBySourceAsync(int rssFeedId)
        {
            return await _articleService.GetAllArticlesBySourceAsync(rssFeedId);
        }
    }
}
