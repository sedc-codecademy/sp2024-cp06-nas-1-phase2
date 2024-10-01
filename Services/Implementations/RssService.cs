using DomainModels;
using Services.Interfaces;
using System.Net.Http;
using System.Xml.Linq;
using DTOs;

namespace Services.Implementations
{

//    public class RssService : IRssService
//   {
//        private readonly HttpClient _httpClient;
//        public RssService()
//        {
//            _httpClient = new HttpClient();
//        }
//        public async Task<List<Dictionary<string, string>>> FetchAndParseRssFeedsAsync(List<RssSourceDto> urls)
//        {
//            var allArticles = new List<Dictionary<string, string>>();

//            foreach (var urlConfig in urls)
//            {
//                try
//                {
//                    var response = await _httpClient.GetAsync(urlConfig.FeedUrl);
//                    response.EnsureSuccessStatusCode();
//                    var xmlContent = await response.Content.ReadAsStringAsync();
//                    var parsedArticles = ParseRss(xmlContent, urlConfig);
//                    allArticles.AddRange(parsedArticles);
//                }
//                catch (Exception ex)
//                {
//                    Console.WriteLine($"Error fetching or parsing RSS feed from {urlConfig.FeedUrl}: {ex.Message}");
//                }
//            }

//            return allArticles;
//        }

//        private IEnumerable<Dictionary<string, string>> ParseRss(string xml, RssSourceDto rssSourceDto)
//        {
//            var filteredXml = xml.Replace("<script", "<removed-script").Replace("</script>", "</removed-script>");
//            var xmlDoc = XDocument.Parse(filteredXml);
//            var items = xmlDoc.Descendants("item");

//            var articles = new List<Dictionary<string, string>>();

//            foreach (var item in items)
//            {
//                var article = new Dictionary<string, string>
//                {
//                    { "title", GetElementText(item, rssSourceDto.Title) },
//                    { "description", StripHtmlTags(GetElementText(item, rssSourceDto.Description)) },
//                    { "link", GetElementText(item, rssSourceDto.Link) },
//                    { "author", GetElementText(item, rssSourceDto.Author) },
//                    { "pubDate", GetElementText(item, rssSourceDto.PubDate) },
//                    { "urlToImage", GetEnclosureUrl(item, rssSourceDto.UrlToImageConfig) }
//                };
//                articles.Add(article);
//            }

//            return articles;

//            /*
//            var items = xmlDoc.Descendants("item");

//            return items.Select(item => new Dictionary<string, string>
//                {
//                    { "title", GetElementText(item, rssSourceDto.Title) },
//                    { "description", StripHtmlTags(GetElementText(item, rssSourceDto.Description)) },
//                    { "link", GetElementText(item, rssSourceDto.Link) },
//                    { "author", GetElementText(item, rssSourceDto.Author) },
//                    { "pubDate", GetElementText(item, rssSourceDto.PubDate) },
//                    { "urlToImage", GetEnclosureUrl(item, rssSourceDto.UrlToImage) }
//                })
//                .ToList();
//             */
//        }
//        private string GetElementText(XElement parent, string tagName)
//        {
//            var element = parent.Element(tagName);
//            return element?.Value?.Trim() ?? "";
//        }

//        private string GetEnclosureUrl(XElement item, UrlToImageConfig urlToImageConfig)
//        {
//            var element = item.Descendants(urlToImageConfig.Query).FirstOrDefault();
//            if (element != null)
//            {
//                if (!string.IsNullOrEmpty(urlToImageConfig.Attribute))
//                {
//                    return element.Attribute(urlToImageConfig.Attribute)?.Value ?? "";
//                }
//                else if (!string.IsNullOrEmpty(urlToImageConfig.Regex))
//                {
//                    var regex = new System.Text.RegularExpressions.Regex(urlToImageConfig.Regex);
//                    var match = regex.Match(element.Value);
//                    if (match.Success && match.Groups.Count > 1)
//                    {
//                        return match.Groups[1].Value;
//                    }
//                }
//                else
//                {
//                    return element.Value.Trim();
//                }
//            }
//            return "";
//        }

//        private string StripHtmlTags(string text)
//        {
//            return System.Text.RegularExpressions.Regex.Replace(text, "<[^>]*>", "");
//        }
//    }
}
