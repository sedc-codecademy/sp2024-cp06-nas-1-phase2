using DomainModels;
using DTOs.UrlToImageConfig;

namespace Services.Interfaces
{
    public interface IUrlToImageConfigService
    {
        Task<IEnumerable<UrlToImageConfigDto>> GetByRssFeedId(int rssFeedId);
        Task AddUrlToImageConfigAsync(UrlToImageConfigDto config);
        Task UpdateUrlToImageConfigAsync(UrlToImageConfig config);
        Task DeleteUrlToImageConfigAsync(int id);
    }
}
