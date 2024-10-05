using DTOs.RssFeed;
using System.Xml.Linq;

namespace Services.Interfaces
{
    public interface IRssFeedService
    {
        Task<IEnumerable<RssFeedDto>> GetAllRssFeedsAsync();
        Task<RssFeedDto> GetRssFeedBySourceAsync(string source);
        Task AddRssFeedWithConfigAsync(AddRssFeedDto addRssFeedDto);
        Task FetchAndProcessRssFeedsAsync();
        //Task<string> FetchRssFeedXmlAsync(string feedUrl);
        //Task<RssFeedDto> GetRssFeedByIdAsync(int id);
        //Task AddRssFeedAsync(AddRssFeedDto rssFeedDto);
        //Task UpdateRssFeedAsync(UpdateRssFeedDto rssFeedDto);
        //Task DeleteRssFeedAsync(int id);
    }
}
