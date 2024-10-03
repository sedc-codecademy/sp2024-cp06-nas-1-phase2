using DomainModels;
using DTOs.RssFeed;

namespace Services.Interfaces
{
    public interface IRssService
    {
        public Task<List<Dictionary<string, string>>> FetchAndParseRssFeedsAsync(List<RssFeedDto> urls);
        //public List<Dictionary<string, string>> ParseRss(string xml, UrlConfig urlConfigOld);
    }
}
