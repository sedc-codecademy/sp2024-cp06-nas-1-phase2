using DTOs.UrlToImageConfig;

namespace Services.Interfaces
{
    public interface IUrlToImageConfigService
    {
        Task<IEnumerable<UrlToImageConfigDto>> GetConfigsByRssSourceIdAsync(int rssSourceId);
        //Task AddUrlToImageConfigAsync(UrlToImageConfig config);
        //Task UpdateUrlToImageConfigAsync(UrlToImageConfig config);
        //Task DeleteUrlToImageConfigAsync(int id);
    }
}
