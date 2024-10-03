using DomainModels;
using Services.Interfaces;
using System.Xml.Linq;
using Microsoft.Extensions.Configuration;
using System.Text.RegularExpressions;

namespace Services.Implementations
{
    /*
    public class ApiServiceOldOld : IApiServiceOLD
    {
        private readonly List<RssFeed> _urls;
        private readonly HttpClient _httpClient;
        //private readonly OpenPageRankService _openPageRankService;
        private readonly IConfiguration _configuration;

        public ApiServiceOldOld(HttpClient httpClient,
            //OpenPageRankService openPageRankService,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            //_openPageRankService = openPageRankService;
            _configuration = configuration;

            _urls = new List<RssFeed>
            {
                new RssFeed
                {
                    Source = "MIA",
                    SourceUrl = "https://mia.mk",
                    FeedUrl = "https://mia.mk/feed",
                    Title = "title",
                    Description = "description",
                    Link = "link",
                    Author = "author",
                    PubDate = "pubDate",
                    //UrlToImage = new List<string> { "enclosure", "url" }
                }//,
                //new UrlConfig
                //{
                //    Source = "Telma",
                //    SourceUrl = "https://telma.com.mk",
                //    FeedUrl = "https://telma.com.mk/feed/",
                //    Title = "title",
                //    Description = "content:encoded",
                //    Link = "link",
                //    Author = "dc:creator", // Example where author tag may vary
                //    PubDate = "pubDate",
                //    UrlToImage = new List<string> { "content:encoded", @"<img[^>]*src=\""([^\""]*)\""" } //<img[^>]*src=\"([^\"]*)\"
                //},
                //new UrlConfig
                //{
                //    Source = "24Vesti",
                //    SourceUrl = "https://24.mk",
                //    FeedUrl = "https://admin.24.mk/api/rss.xml",
                //    Title = "title",
                //    Description = "content",
                //    Link = "link",
                //    Author = "",
                //    PubDate = "pubDate",
                //    UrlToImage = new List<string> { "img", "src" },
                //},
                //new UrlConfig
                //{
                //    Source = "Sitel",
                //    SourceUrl = "https://sitel.com.mk",
                //    FeedUrl = "https://sitel.com.mk/rss.xml",
                //    Title = "title",
                //    Description = "description",
                //    Link = "link",
                //    Author = "dc:creator",
                //    PubDate = "pubDate",
                //    UrlToImage = new List<string> { "description", @"<img[^>]*src=\""([^\""]*)\""" }
                //},
                //new UrlConfig
                //{
                //    Source = "Kanal5",
                //    SourceUrl = "https://kanal5.com.mk",
                //    FeedUrl = "https://kanal5.com.mk/rss.aspx",
                //    Title = "title",
                //    Description = "content",
                //    Link = "link",
                //    Author = "author", // Example where author tag may be different
                //    PubDate = "pubDate",
                //    UrlToImage = new List<string> { "thumbnail", "" }
                //}
            };
        }
        public async Task<List<Article>> FetchRssFeedsAsync()
        {
            var allArticles = new List<Article>();

            foreach (var urlConfig in _urls)
            {
                try
                {
                    var xmlData = await FetchRssFeedXmlAsync(urlConfig.FeedUrl);
                    var parsedArticles = ParseRss(xmlData, urlConfig);

                    foreach (var article in parsedArticles)
                    {
                        //article.TrustScore = await _openPageRankService.GetTrustScoreAsync(article.SourceUrl);
                        Console.WriteLine(article.TrustScore);
                    }
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

        private List<Article> ParseRss(string xmlData, RssFeed urlConfig)
        {
            var articles = new List<Article>();

            try
            {
                xmlData = RemoveScriptTags(xmlData);
                var xmlDoc = XDocument.Parse(xmlData);

                foreach (var item in xmlDoc.Descendants("item"))
                {
                    if (item.Descendants("script").Any())
                    {
                        continue;
                    }

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
                        //UrlToImage = ExtractImageUrl(item, urlConfig.UrlToImage)
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

        private static string GetElementValue(XElement parent, string tagName)
        {
            if (tagName == "")
            {
                return "";
            }

            XElement element = FindElementByTagName(parent, tagName);
            return element?.Value.Trim() ?? string.Empty;
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

        private static bool IsValidRegex(string pattern)
        {
            if (string.IsNullOrWhiteSpace(pattern))
            {
                return false;
            }

            // Check if the pattern contains typical regex metacharacters
            if (!ContainsRegexMetaCharacters(pattern))
            {
                return false;
            }

            try
            {
                // Try to create a Regex object from the pattern
                Regex.Match(string.Empty, pattern);
            }
            catch (ArgumentException)
            {
                // If an ArgumentException is thrown, the pattern is not a valid regex
                return false;
            }

            return true;
        }

        private static bool ContainsRegexMetaCharacters(string pattern)
        {
            // List of common regex metacharacters
            string[] regexMetaCharacters = { ".", "^", "$", "*", "+", "?", "(", ")", "[", "]", "{", "}", "\\", "|" };

            foreach (var metaChar in regexMetaCharacters)
            {
                if (pattern.Contains(metaChar))
                {
                    return true;
                }
            }

            return false;
        }

        private string ExtractImageUrl(XElement parent, IReadOnlyList<string> imageTags)
        {
            //Regex for UrlToImage!!
            //var checkRegex = new Regex(imageTags[1]);
            //var check = IsValidRegex(imageTags[1]);
            if (imageTags[1] != "")
            {
                if (!IsValidRegex(imageTags[1]))
                {
                    // Scenario 2: Single XML tag with attribute
                    var xmlTag = imageTags[0].Trim();

                    var xmlTagElement = FindElementByTagName(parent, xmlTag);

                    if (xmlTagElement != null)
                    {
                        var xmlAttribute = imageTags[1].Trim(); // Get the attribute name dynamically
                        var attributeValue = xmlTagElement.Attribute(xmlAttribute)?.Value ?? xmlTagElement.Value;

                        if (!string.IsNullOrEmpty(attributeValue))
                        {
                            return attributeValue;
                        }
                    }
                }
                else // if (imageTags.Count == 2 && !string.IsNullOrEmpty(imageTags[1].Trim()))
                {
                    // Scenario 3: Tag with regex
                    var xmlTag = imageTags[0].Trim();
                    var regexPattern = imageTags[1].Trim();

                    var xmlTagElement = FindElementByTagName(parent, xmlTag);

                    if (xmlTagElement != null)
                    {
                        var attributeValue = xmlTagElement.Attribute("url")?.Value ?? xmlTagElement.Value;

                        if (!string.IsNullOrEmpty(attributeValue))
                        {
                            var regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
                            var match = regex.Match(attributeValue);

                            if (match.Success && match.Groups.Count > 1)
                            {
                                return match.Groups[1].Value;
                            }
                        }
                    }
                }
            }
            else // if (imageTags.Count == 1)
            {
                // Scenario 1: One XML tag
                var xmlTag = imageTags[0].Trim();
                //var xmlTag2 = imageTags[1].Trim();

                var xmlTagElement = FindElementByTagName(parent, xmlTag);
                //var xmlTagElement2 = FindElementByTagName(parent, xmlTag2);

                if (xmlTagElement != null)// && xmlTagElement2 != null)
                {
                    return xmlTagElement?.Value;// ?? xmlTagElement2.Value;
                }
            }

            return string.Empty;
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
    */
}
