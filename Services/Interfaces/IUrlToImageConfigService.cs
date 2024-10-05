using DomainModels;
using DTOs.UrlToImageConfig;
using System.Xml.Linq;

namespace Services.Interfaces
{
    public interface IUrlToImageConfigService
    {
        Task<string> GetImageUrl(XElement item, int rssSourceId);
        Task<UrlToImageConfigDto> GetByRssFeedId(int rssFeedId);
        Task AddUrlToImageConfigAsync(UrlToImageConfigDto config);
        Task UpdateUrlToImageConfigAsync(UrlToImageConfig config);
        Task DeleteUrlToImageConfigAsync(int id);
    }
}
