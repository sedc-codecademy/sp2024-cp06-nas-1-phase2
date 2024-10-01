using DomainModels;
using DTOs.RssSource;

namespace Services.Interfaces
{
    public interface IRssSourceService
    {
        Task<IEnumerable<RssSourceDto>> GetAllRssSourcesAsync();
        Task<RssSourceDto> GetRssSourceByIdAsync(int id);
        Task AddRssSourceAsync(AddRssSourceDto rssSource);
        Task UpdateRssSourceAsync(UpdateRssSourceDto rssSource);
        Task DeleteRssSourceAsync(int id);
    }
}
