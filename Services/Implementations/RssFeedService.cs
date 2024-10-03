using System.Net.Http;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using AutoMapper;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.RssFeed;
using Services.Interfaces;

namespace Services.Implementations
{
    public class RssFeedService : IRssFeedService
    {
        private readonly IRssFeedRepository _rssFeedRepository;
        private readonly IUrlToImageConfigService _urlToImageConfigService;
        private readonly IMapper _mapper;
        private readonly HttpClient _httpClient;

        public RssFeedService(IRssFeedRepository rssFeedRepository,
            IUrlToImageConfigService urlToImageConfigService,
            IMapper mapper,
            HttpClient httpClient)
        {
            _rssFeedRepository = rssFeedRepository;
            _urlToImageConfigService = urlToImageConfigService;
            _mapper = mapper;
            _httpClient = httpClient;
        }
        public async Task<IEnumerable<RssFeedDto>> GetAllRssFeedsAsync()
        {
            try
            {
                var rssSources = await _rssFeedRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<RssFeedDto>>(rssSources);
                //var rssSourcesDtos = rssSources.Select(x => _mapper.Map<RssFeedDto>(x)).ToList();
                //return rssSourcesDtos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<RssFeedDto> GetRssFeedByIdAsync(int id)
        {
            try
            {
                var source = await _rssFeedRepository.GetByIdAsync(id);
                if (source == null)
                {
                    throw new Exception();
                }

                //var mapperSource = 
                return _mapper.Map<RssFeedDto>(source);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Article> CreateArticles(List<XElement> items, RssFeed rssFeed)
        {
            var articles = new List<Article>();
            foreach (var item in items)
            {
                var article = new Article
                {
                    RssFeedId = rssFeed.Id,
                    FeedUrl = rssFeed.FeedUrl,
                    Title = GetElementValue(item, rssFeed.Title),
                    Description = StripHtmlTags(GetElementValue(item, rssFeed.Description)),
                    Link = GetElementValue(item, rssFeed.Link),
                    Author = GetElementValue(item, rssFeed.Author),
                    PubDate = GetElementValue(item, rssFeed.PubDate),
                };
                articles.Add(article);
            }
            return articles;
        }
        private static string GetElementValue(XElement parent, string tagName)
        {
            if (string.IsNullOrEmpty(tagName)) return string.Empty;
            XElement element = parent.Descendants(tagName).FirstOrDefault();
            return element?.Value.Trim() ?? string.Empty;
        }
        private static string StripHtmlTags(string text)
        {
            return Regex.Replace(text, "<.*?>", string.Empty);
        }
        public async Task<string> FetchRssFeedXmlAsync(string feedUrl)
        {
            try
            {
                var response = await _httpClient.GetAsync(feedUrl);
                response.EnsureSuccessStatusCode();

                var xmlContent = await response.Content.ReadAsStringAsync();
                return xmlContent;
            }
            catch (Exception ex)
            {
                // Log and handle exceptions as necessary
                throw new Exception($"Error fetching RSS feed from {feedUrl}: {ex.Message}", ex);
            }
        }
        public async Task<string> ExtractImageUrl(XElement item, RssFeedDto rssFeed)
        {
            var urlToImageConfigs = await _urlToImageConfigService.GetByRssFeedId(rssFeed.Id);

            foreach (var config in urlToImageConfigs)
            {
                // If Attribute is set, try extracting the image URL from the attribute of the element
                if (!string.IsNullOrEmpty(config.Attribute))
                {
                    var element = item.Descendants(config.Query).FirstOrDefault();
                    if (element != null && element.Attribute(config.Attribute) != null)
                    {
                        return element.Attribute(config.Attribute).Value;
                    }
                }

                // If Regex is set, try matching the regex to find an image URL
                if (!string.IsNullOrEmpty(config.Regex))
                {
                    var match = Regex.Match(item.ToString(), config.Regex);
                    if (match.Success)
                    {
                        return match.Value;
                    }
                }
            }

            // Default to null if no image URL was found
            return null;
        }
        public async Task AddRssFeedAsync(AddRssFeedDto rssFeedDto)
        {
            try
            {
                var addRssFeed = _mapper.Map<RssFeed>(rssFeedDto);

                await _rssFeedRepository.AddAsync(addRssFeed);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task UpdateRssFeedAsync(UpdateRssFeedDto rssFeedDto)
        {
            try
            {
                var rssSource = _mapper.Map<RssFeed>(rssFeedDto);

                await _rssFeedRepository.UpdateAsync(rssSource);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task DeleteRssFeedAsync(int id)
        {
            try
            {
                var rssSource = _rssFeedRepository.GetByIdAsync(id).ToString();
                if (rssSource == null)
                {
                    throw new Exception();
                }

                await _rssFeedRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }

}
