using DataAccess.Interfaces;
using DomainModels;
using Services.Interfaces;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using AutoMapper;
using DTOs.Article;

namespace Services.Implementations
{
    public class ApiService : IApiService
    {
        private readonly IRssSourceRepository _rssSourceRepository;
        private readonly IArticleRepository _articleRepository;
        private readonly IUrlToImageConfigRepository _urlToImageConfigRepository;
        private readonly HttpClient _httpClient;
        private readonly IMapper _mapper;

        public ApiService(IRssSourceRepository rssSourceRepository,
            IArticleRepository articleRepository,
            IUrlToImageConfigRepository urlToImageConfigRepository,
            HttpClient httpClient,
            IMapper mapper)
        {
            _rssSourceRepository = rssSourceRepository;
            _articleRepository = articleRepository;
            _urlToImageConfigRepository = urlToImageConfigRepository;
            _httpClient = httpClient;
            _mapper = mapper;
        }
        public async Task<List<ArticleDto>> FetchRssFeedsAsync()
        {
            var allArticles = new List<Article>();

            // Retrieve RSS feeds from the database
            var rssFeeds = await _rssSourceRepository.GetAllAsync();

            foreach (var rssSource in rssFeeds)
            {
                try
                {
                    var xmlData = await FetchRssFeedXmlAsync(rssSource.FeedUrl);
                    var parsedArticles = ParseRss(xmlData, rssSource);

                    allArticles.AddRange(parsedArticles);

                    foreach (var article in parsedArticles)
                    {
                        await _articleRepository.AddAsync(article);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching or parsing RSS feed from {rssSource.Source}: {ex.Message}");
                }
            }

            var allArticlesDto = allArticles.Select(x => _mapper.Map<ArticleDto>(x)).ToList();
            return allArticlesDto;
        }
        /*
        private void ExtractImagesForArticles(List<Article> articles, List<XElement> items,
            ICollection<UrlToImageConfig> imageConfigs)
        {
            for (int i = 0; i < articles.Count; i++)
            {
                foreach (var config in imageConfigs)
                {
                    var imageElement = items[i].Descendants(config.Query).FirstOrDefault();
                    if (imageElement != null)
                    {
                        if (!string.IsNullOrEmpty(config.Attribute))
                        {
                            articles[i].UrlToImage = imageElement.Attribute(config.Attribute)?.Value ?? string.Empty;
                        }
                        else if (!string.IsNullOrEmpty(config.Regex))
                        {
                            var regex = new Regex(config.Regex);
                            var match = regex.Match(imageElement.Value);
                            articles[i].UrlToImage = match.Success ? match.Value : string.Empty;
                        }
                    }
                }
            }
        }
        */
        private async Task<string> FetchRssFeedXmlAsync(string feedUrl)
        {
            var response = await _httpClient.GetAsync(feedUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        /*
        private static async Task<string> FetchRssFeedXmlAsync(string feedUrl)
        {
            using (var httpClient = new HttpClient())
            {
                // Set the User-Agent header to your desired value
                httpClient.DefaultRequestHeaders.Add("User-Agent", "RSSFetcher/1.0");

                var response = await httpClient.GetAsync(feedUrl);
                response.EnsureSuccessStatusCode();

                return await response.Content.ReadAsStringAsync();
            }
        }
        */
        private List<Article> ParseRss(string xmlData, RssSource rssSource)
        {
            var articles = new List<Article>();

            try
            {
                xmlData = RemoveScriptTags(xmlData);
                var xmlDoc = XDocument.Parse(xmlData);

                foreach (var item in xmlDoc.Descendants("item"))
                {
                    var article = new Article
                    {
                        RssSourceId = rssSource.Id,
                        Title = GetElementValue(item, rssSource.Title),
                        Description = StripHtmlTags(GetElementValue(item, rssSource.Description)),
                        Link = GetElementValue(item, rssSource.Link),
                        Author = GetElementValue(item, rssSource.Author),
                        PubDate = GetElementValue(item, rssSource.PubDate),
                        FeedUrl = rssSource.FeedUrl

                        //Source = rssSource.Source,
                        //SourceUrl = rssSource.SourceUrl,
                        //FeedUrl = rssSource.FeedUrl,
                        //Title = GetElementValue(item, rssSource.Title),
                        //Description = StripHtmlTags(GetElementValue(item, rssSource.Description)),
                        //Link = GetElementValue(item, rssSource.Link),
                        //Author = GetElementValue(item, rssSource.Author),
                        //PubDate = GetElementValue(item, rssSource.PubDate),
                    };
                    var urlToImageConfigs = _urlToImageConfigRepository.GetConfigsByRssSourceIdAsync(rssSource.Id).Result;
                    article.UrlToImage = ExtractImageUrl(item, urlToImageConfigs);
                    articles.Add(article);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing RSS feed: {ex.Message}");
            }

            return articles;
        }
        private static string ExtractImageUrl(XElement item, IEnumerable<UrlToImageConfig> urlToImageConfigs)
        {
            foreach (var config in urlToImageConfigs)
            {
                var element = item.Descendants(config.Query).FirstOrDefault();
                if (element != null)
                {
                    if (!string.IsNullOrEmpty(config.Attribute))
                    {
                        return element.Attribute(config.Attribute)?.Value ?? string.Empty;
                    }
                    if (!string.IsNullOrEmpty(config.Regex))
                    {
                        var match = Regex.Match(element.Value, config.Regex);
                        if (match.Success)
                        {
                            return match.Value;
                        }
                    }
                }
            }
            return string.Empty;
        }
        /*
        private static XElement FindElementByTagName(XElement parent, string tagName)
        {
            if (tagName.Contains(":"))
            {
                var parts = tagName.Split(':');
                var prefix = parts[0];
                var localName = parts[1];
                var ns = parent.GetNamespaceOfPrefix(prefix);
                return parent.Descendants(ns + localName).FirstOrDefault();
            }
            else
            {
                return parent.Descendants(tagName).FirstOrDefault();
            }
        }
        */
        private static string GetElementValue(XElement parent, string tagName)
        {
            if (string.IsNullOrEmpty(tagName)) return string.Empty;
            XElement element = parent.Descendants(tagName).FirstOrDefault();
            return element?.Value.Trim() ?? string.Empty;
            //if (tagName == "")
            //{
            //    return "";
            //}

            //XElement element = FindElementByTagName(parent, tagName);
            //return element?.Value.Trim() ?? string.Empty;
        }
        private static string StripHtmlTags(string text, List<string> allowedTags = null)
        {
            if (allowedTags == null || allowedTags.Count == 0)
            {
                return Regex.Replace(text, "<.*?>", string.Empty);
            }

            var tagList = string.Join("|", allowedTags);
            var regex = new Regex($"<(?!/?(?:{tagList})\\b)[^>]*>", RegexOptions.IgnoreCase);
            return regex.Replace(text, string.Empty);
        }
        private static string RemoveScriptTags(string xmlData)
        {
            return Regex.Replace(xmlData, "<script.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }
    }
}
