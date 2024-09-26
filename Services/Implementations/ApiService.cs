using DataAccess;
using DomainModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Services.Implementations
{
    public class ApiService : IApiService
    {
        private readonly NewsAggregatorDbContext _dbContext;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public ApiService(NewsAggregatorDbContext dbContext,
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _dbContext = dbContext;
            _httpClient = httpClient;
            _configuration = configuration;
        }
        public async Task<List<Article>> FetchRssFeedsAsync()
        {
            var allArticles = new List<Article>();

            // Retrieve RSS feeds from the database
            var rssFeeds = await _dbContext.RssSources.ToListAsync();

            foreach (var urlConfig in rssFeeds)
            {
                try
                {
                    var xmlData = await FetchRssFeedXmlAsync(urlConfig.FeedUrl);
                    var parsedArticles = ParseRss(xmlData, urlConfig);

                    allArticles.AddRange(parsedArticles);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error fetching or parsing RSS feed from {urlConfig.Source}: {ex.Message}");
                }
            }

            return allArticles;
        }

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
        private List<Article> ParseRss(string xmlData, RssSource urlConfig)
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
                        Source = urlConfig.Source,
                        SourceUrl = urlConfig.SourceUrl,
                        FeedUrl = urlConfig.FeedUrl,
                        Title = GetElementValue(item, urlConfig.Title),
                        Description = StripHtmlTags(GetElementValue(item, urlConfig.Description)),
                        Link = GetElementValue(item, urlConfig.Link),
                        Author = GetElementValue(item, urlConfig.Author),
                        PubDate = GetElementValue(item, urlConfig.PubDate),
                    };
                    articles.Add(article);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing RSS feed: {ex.Message}");
            }

            return articles;
        }
        private static string RemoveScriptTags(string xmlData)
        {
            return Regex.Replace(xmlData, "<script.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }
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
        private static string GetElementValue(XElement parent, string tagName)
        {
            if (tagName == "")
            {
                return "";
            }

            XElement element = FindElementByTagName(parent, tagName);
            return element?.Value.Trim() ?? string.Empty;
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
    }
}
