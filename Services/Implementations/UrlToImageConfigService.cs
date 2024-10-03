using AutoMapper;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.UrlToImageConfig;
using Services.Interfaces;

namespace Services.Implementations
{
    public class UrlToImageConfigService : IUrlToImageConfigService
    {
        private readonly IUrlToImageConfigRepository _configRepository;
        private readonly IMapper _mapper;

        public UrlToImageConfigService(IUrlToImageConfigRepository configRepository, IMapper mapper)
        {
            _configRepository = configRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UrlToImageConfigDto>> GetByRssFeedId(int rssFeedId)
        {
            try
            {
                var configs = await _configRepository.GetByIdAsync(rssFeedId);
                return _mapper.Map<IEnumerable<UrlToImageConfigDto>>(configs);
                //var urlToImageConfig = await _urlToImageConfigRepository.GetConfigsByRssSourceIdAsync(rssFeedId);

                //if (rssFeedId < 0 || rssFeedId > urlToImageConfig.Count())
                //{
                //    throw new Exception();
                //}

                //var urlToImageConfigDto = urlToImageConfig.Select(url => _mapper.Map<UrlToImageConfigDto>(url)).ToList();

                //return urlToImageConfigDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task AddUrlToImageConfigAsync(UrlToImageConfigDto configDto)
        {
            try
            {
                var config = _mapper.Map<UrlToImageConfig>(configDto);
                await _configRepository.AddAsync(config);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateUrlToImageConfigAsync(UrlToImageConfig configDto)
        {
            try
            {
                var config = _mapper.Map<UrlToImageConfig>(configDto);
                await _configRepository.UpdateAsync(config);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteUrlToImageConfigAsync(int id)
        {
            try
            {
                await _configRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
