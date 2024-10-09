using System.Xml.Linq;
using AutoMapper;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.Article;
using DTOs.RssFeed;
using Services.Interfaces;

namespace Services.Implementations
{
    public class RssFeedService : IRssFeedService
    {
        private readonly IRssFeedRepository _rssFeedRepository;
        private readonly IUrlToImageConfigService _urlToImageConfigService;
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;

        public RssFeedService(
            IRssFeedRepository rssFeedRepository,
            IUrlToImageConfigService urlToImageConfigService,
            IArticleService articleService,
            IMapper mapper)
        {
            _rssFeedRepository = rssFeedRepository;
            _urlToImageConfigService = urlToImageConfigService;
            _articleService = articleService;
            _mapper = mapper;
        }

        #region GenericMethods

        public async Task<IEnumerable<RssFeedDto>> GetAllRssFeedsAsync()
        {
            try
            {
                var rssSources = await _rssFeedRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<RssFeedDto>>(rssSources);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<RssFeedDto> GetRssFeedBySourceAsync(string source)
        {
            try
            {
                var feed = await _rssFeedRepository.GetBySourceAsync(source);
                if (source == null)
                {
                    throw new Exception();
                }

                return _mapper.Map<RssFeedDto>(feed);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task AddRssFeedWithConfigAsync(AddRssFeedDto addRssFeedDto)
        {
            var rssFeed = _mapper.Map<RssFeed>(addRssFeedDto);
            await _rssFeedRepository.AddAsync(rssFeed);

            if (addRssFeedDto.UrlToImageConfig != null)
            {
                var urlConfig = addRssFeedDto.UrlToImageConfig;//_mapper.Map<UrlToImageConfig>(addRssFeedDto.UrlToImageConfig);
                urlConfig.RssFeedId = rssFeed.Id;
                await _urlToImageConfigService.AddUrlToImageConfigAsync(urlConfig);
            }
        }

        //public async Task AddRssFeedAsync(AddRssFeedDto rssFeedDto)
        //{
        //    try
        //    {
        //        var addRssFeed = _mapper.Map<RssFeed>(rssFeedDto);

        //        await _rssFeedRepository.AddAsync(addRssFeed);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}
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

        #endregion

        #region ProcessingXML

        public async Task FetchAndProcessRssFeedsAsync()
        {
            // Step 1: Fetch all RSS sources from the repository
            var rssFeeds = await _rssFeedRepository.GetAllAsync();
            //var rssFeeds2 = rssFeeds.Where(x => x.Id == 1);
            //IEnumerable<ArticleDto> articles = new List<ArticleDto>();
            foreach (var rssFeed in rssFeeds)
            {
                // Step 2: Fetch the XML data from the RSS feed URL
                var xmlData = await FetchRssDataAsync(rssFeed.FeedUrl);

                // Step 3: Parse the XML into articles
                var articles = await ParseRssToArticles(xmlData, rssFeed);

                // Step 4: Add the articles to the database
                await _articleService.AddArticlesAsync(articles);
            }
        }
        private async Task<XDocument> FetchRssDataAsync(string feedUrl)
        {
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetStringAsync(feedUrl);
                return XDocument.Parse(response);
            }
        }
        private async Task<IEnumerable<ArticleDto>> ParseRssToArticles(XDocument xmlData, RssFeed rssFeed)
        {
            var articles = new List<ArticleDto>();
            var items = xmlData.Descendants("item");

            foreach (var item in items)
            {
                // Call UrlToImageConfigService to process image extraction
                var imageUrl = await _urlToImageConfigService.GetImageUrl(item, rssFeed.Id);

                // Create the Article DTO
                var article = new ArticleDto
                {
                    RssFeedId = rssFeed.Id,
                    FeedUrl = rssFeed.FeedUrl,
                    Title = item.Element("title")?.Value,
                    Description = StripHtmlTags(item.Element("description")?.Value),
                    Link = item.Element("link")?.Value,
                    Author = item.Element("author")?.Value,
                    PubDate = item.Element("pubDate")?.Value,
                    UrlToImage = imageUrl
                };

                articles.Add(article);
            }

            return articles;
        }

        private string StripHtmlTags(string text)
        {
            if (string.IsNullOrEmpty(text)) return string.Empty;
            return System.Text.RegularExpressions.Regex.Replace(text, "<.*?>", string.Empty);
        }
        #endregion
    }

}
