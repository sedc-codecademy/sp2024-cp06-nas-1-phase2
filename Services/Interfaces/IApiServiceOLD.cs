using DomainModels;

namespace Services.Interfaces
{
    public interface IApiServiceOLD
    {
        public Task<List<Article>> FetchRssFeedsAsync();
    }
}
