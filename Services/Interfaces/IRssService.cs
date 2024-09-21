using DomainModels;
using DTOs;

namespace Services.Interfaces
{
    public interface IRssService
    {
        public Task<List<Dictionary<string, string>>> FetchAndParseRssFeedsAsync(List<RssSourceDto> urls);
        //public List<Dictionary<string, string>> ParseRss(string xml, UrlConfig urlConfigOld);
    }
}
