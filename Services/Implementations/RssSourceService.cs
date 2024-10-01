using AutoMapper;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.RssSource;
using Mappers;
using Services.Interfaces;

namespace Services.Implementations
{
    public class RssSourceService : IRssSourceService
    {
        private readonly IRssSourceRepository _rssSourceRepository;
        private readonly IMapper _mapper;

        public RssSourceService(IRssSourceRepository rssSourceRepository, IMapper mapper)
        {
            _rssSourceRepository = rssSourceRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RssSourceDto>> GetAllRssSourcesAsync()
        {
            try
            {
                var rssSources = await _rssSourceRepository.GetAllAsync();
                var rssSourcesDtos = rssSources.Select(x => _mapper.Map<RssSourceDto>(x)).ToList();
                return rssSourcesDtos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RssSourceDto> GetRssSourceByIdAsync(int id)
        {
            try
            {
                var source = _rssSourceRepository.GetByIdAsync(id);
                if (source == null)
                {
                    throw new Exception();
                }

                var mapperSource = _mapper.Map<RssSourceDto>(source);
                return mapperSource;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddRssSourceAsync(AddRssSourceDto rssSource)
        {
            try
            {
                var addRssSource = _mapper.Map<RssSource>(rssSource);

                await _rssSourceRepository.AddAsync(addRssSource);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task UpdateRssSourceAsync(UpdateRssSourceDto rssSource)
        {
            try
            {
                var addRssSource = _mapper.Map<RssSource>(rssSource);

                await _rssSourceRepository.UpdateAsync(addRssSource);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteRssSourceAsync(int id)
        {
            try
            {
                var rssSource = _rssSourceRepository.GetByIdAsync(id).ToString();
                if (rssSource == null)
                {
                    throw new Exception();
                }

                await _rssSourceRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
