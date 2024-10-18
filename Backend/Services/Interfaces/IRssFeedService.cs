using DTOs.Article;
using DTOs.RssFeed;
using System.Xml.Linq;

namespace Services.Interfaces
{
    public interface IRssFeedService
    {
        Task<IEnumerable<RssFeedDto>> GetAllRssFeedsAsync();
        Task<RssFeedDto> GetRssFeedBySourceAsync(string source);
        Task<List<ArticleDto>> FetchAndProcessRssFeedsAsync(CancellationToken cancellationToken);
        //Task<string> FetchRssFeedXmlAsync(string feedUrl);
        //Task<RssFeedDto> GetRssFeedByIdAsync(int id);
        Task AddRssFeedAsync(AddRssFeedDto rssFeedDto);
        //Task UpdateRssFeedAsync(UpdateRssFeedDto rssFeedDto);
        //Task DeleteRssFeedAsync(int id);
    }
}
