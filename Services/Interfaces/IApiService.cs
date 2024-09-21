using DomainModels;

namespace Services.Interfaces
{
    public interface IApiService
    {
        public Task<List<Article>> FetchRssFeedsAsync();
    }
}
