using DTOs.Article;

namespace Services.Interfaces
{
    public interface IApiService
    {
        public Task<List<ArticleDto>> FetchRssFeedsAsync();
    }
}
