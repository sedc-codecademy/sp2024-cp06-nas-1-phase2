using AutoMapper;
using DataAccess.Interfaces;
using DTOs.UrlToImageConfig;
using Services.Interfaces;

namespace Services.Implementations
{
    public class UrlToImageConfigService : IUrlToImageConfigService
    {
        private readonly IUrlToImageConfigRepository _urlToImageConfigRepository;
        private readonly IMapper _mapper;

        public UrlToImageConfigService(IUrlToImageConfigRepository urlToImageConfigRepository, IMapper mapper)
        {
            _urlToImageConfigRepository = urlToImageConfigRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UrlToImageConfigDto>> GetConfigsByRssSourceIdAsync(int rssSourceId)
        {
            try
            {
                var urlToImageConfig = await _urlToImageConfigRepository.GetConfigsByRssSourceIdAsync(rssSourceId);

                if (rssSourceId < 0 || rssSourceId > urlToImageConfig.Count())
                {
                    throw new Exception();
                }

                var urlToImageConfigDto = urlToImageConfig.Select(url => _mapper.Map<UrlToImageConfigDto>(url)).ToList();

                return urlToImageConfigDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
