using System.Globalization;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using AutoMapper;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.Article;
using DTOs.RssFeed;
using Services.Interfaces;
using Shared.Modules;

namespace Services.Implementations
{
    public class RssFeedService : IRssFeedService
    {
        private readonly IRssFeedRepository _rssFeedRepository;
        private readonly IArticleService _articleService;
        private readonly IMapper _mapper;
        private readonly ILoggerHelper _logger;

        public RssFeedService(
            IRssFeedRepository rssFeedRepository,
            IArticleService articleService,
            IMapper mapper,
            ILoggerHelper logger)
        {
            _rssFeedRepository = rssFeedRepository;
            _articleService = articleService;
            _mapper = mapper;
            _logger = logger;
        }

        #region GenericMethods

        public async Task<IEnumerable<RssFeedDto>> GetAllRssFeedsAsync()
        {
            try
            {
                var rssFeeds = await _rssFeedRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<RssFeedDto>>(rssFeeds);
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
                return feed == null ? throw new Exception() : _mapper.Map<RssFeedDto>(feed);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task AddRssFeedAsync(AddRssFeedDto rssFeedDto)
        {
            try
            {
                var rssFeed = _mapper.Map<RssFeed>(rssFeedDto);
                await _rssFeedRepository.AddAsync(rssFeed);
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
                if (rssSource != null)
                    await _rssFeedRepository.DeleteAsync(id);
                else
                    throw new Exception();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<ArticleDto>> FetchAndProcessRssFeedsAsync(CancellationToken cancellationToken)
        {
            try
            {
                var rssFeeds = await _rssFeedRepository.GetAllAsync();
                //var rssFeeds = rssFeeds1.Where(x => x.Id == 5);
                var articles = new List<Article>();

                foreach (var rssFeed in rssFeeds)
                {
                    /*
                    var image = new List<string>();
                    if (!string.IsNullOrEmpty(rssFeed.Query))
                    {
                        image.Add(rssFeed.Query);
                    }

                    if (!string.IsNullOrEmpty(rssFeed.Attribute))
                    {
                        image.Add(rssFeed.Attribute);
                    }

                    if (!string.IsNullOrEmpty(rssFeed.Regex))
                    {
                        image.Add(rssFeed.Regex);
                    }
                    */
                    var image = new List<string>
                    {
                        rssFeed.Query,
                        rssFeed.Attribute!,
                        rssFeed.Regex!
                    }.Where(item => !string.IsNullOrEmpty(item)).ToList();

                    var xmlContent = await FetchRssXmlAsync(rssFeed.FeedUrl);
                    var items = ParseRssItems(xmlContent);

                    /*
                    articles.AddRange(items.Select(item => new Article
                    {
                        Title = item.Element("title")?.Value!,
                        Description = StripHtmlTags(item.Element("description")?.Value!),
                        Link = item.Element("link")?.Value!,
                        Author = item.Element("author")?.Value!,
                        PubDate = item.Element("pubDate")?.Value ?? DateTime.Now.ToString(CultureInfo.CurrentCulture),
                        FeedUrl = rssFeed.FeedUrl,
                        RssFeedId = rssFeed.Id,
                        UrlToImage = GetImageUrl(item, image)
                    }));
                    */

                    foreach (var item in items)
                    {
                        if (item.IsEmpty || string.IsNullOrEmpty(item.Element("title")?.Value))
                        {
                            _logger.LogInfo($"Empty or invalid item found in feed: {rssFeed.FeedUrl}");
                            continue;
                        }

                        var pubDateString = item.Element("pubDate")?.Value;
                        var pubDate = DateParser.ParseDate(pubDateString) ?? DateTime.Now; // Use current date if parsing fails

                        var article = new Article
                        {
                            Title = item.Element("title")?.Value!,
                            Description = StripHtmlTags(item.Element("description")?.Value!),
                            Link = item.Element("link")?.Value!,
                            Author = item.Element("author")?.Value!,
                            PubDate = pubDate.ToString("dd-MM-yyyy HH:mm", CultureInfo.CurrentCulture),
                            FeedUrl = rssFeed.FeedUrl,
                            RssFeedId = rssFeed.Id,
                            UrlToImage = GetImageUrl(item, image) // Extract image URL based on the RssFeed's properties
                        };

                        articles.Add(article);
                    }
                }

                var articlesDto = articles.Select(article => _mapper.Map<ArticleDto>(article)).ToList();
                await _articleService.AddArticlesAsync(articlesDto, cancellationToken);
                return articlesDto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        #endregion

        #region PrivateMethods

        private async Task<string> FetchRssXmlAsync(string feedUrl)
        {
            try
            {
                using var client = new HttpClient();
                //client.DefaultRequestHeaders.Add("User-Agent", "NewsAggregator");
                client.DefaultRequestHeaders.Add("User-Agent",
                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:131.0) Gecko/20100101 Firefox/131.0");
                var response = await client.GetStringAsync(feedUrl);

                response = RemoveScriptTags(response);
                if (string.IsNullOrEmpty(response))
                {
                    throw new Exception("The response is either null or empty.");
                }
                _logger.LogInfo($"Successfully fetched xml from source: {feedUrl}");
                return response;
            }
            catch (HttpRequestException ex) when (ex.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                _logger.LogError(ex, $"403 Forbidden error fetching from {feedUrl}. Returning empty <item> tag.");
                return "<item></item>";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Received invalid response format; expected XML. Error fetching from {feedUrl}");
                Console.WriteLine("Received invalid response format; expected XML. Error fetching from {feedUrl}");
                return "<item></item>";
            }
        }
        private static XElement FindElementByTagName(XElement parent, string tagName)
        {
            if (tagName.Contains(':'))
            {
                var parts = tagName.Split(':');
                var prefix = parts[0];
                var localName = parts[1];
                var ns = parent.GetNamespaceOfPrefix(prefix);
                return parent.Descendants(ns + localName).FirstOrDefault();
            }
            else
            {
                return parent.Descendants(tagName).FirstOrDefault() ?? throw new InvalidOperationException();
            }
        }
        private static IEnumerable<XElement> ParseRssItems(string xmlContent)
        {
            try
            {
                var xDoc = XDocument.Parse(xmlContent);
                return xDoc.Descendants("item").ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        private static string GetImageUrl(XElement item, IReadOnlyList<string> imageTags)
        {
            try
            {
                if (imageTags[1] != "")
                {
                    if (!IsValidRegex(imageTags[1]))
                    {
                        // Scenario 2: Single XML tag with attribute
                        var xmlTag = imageTags[0].Trim();

                        var xmlTagElement = FindElementByTagName(item, xmlTag);

                        if (xmlTagElement == null) return string.Empty;
                        var xmlAttribute = imageTags[1].Trim(); // Get the attribute name dynamically
                        var attributeValue = xmlTagElement.Attribute(xmlAttribute)?.Value ?? xmlTagElement.Value;

                        if (!string.IsNullOrEmpty(attributeValue))
                        {
                            return attributeValue;
                        }
                    }
                    else // if (imageTags.Count == 2 && !string.IsNullOrEmpty(imageTags[1].Trim()))
                    {
                        // Scenario 3: Tag with regex
                        var xmlTag = imageTags[0].Trim();
                        var regexPattern = imageTags[1].Trim();

                        var xmlTagElement = FindElementByTagName(item, xmlTag);

                        if (xmlTagElement == null) return string.Empty;
                        var attributeValue = xmlTagElement.Attribute("url")?.Value ?? xmlTagElement.Value;

                        if (string.IsNullOrEmpty(attributeValue)) return string.Empty;
                        var regex = new Regex(regexPattern, RegexOptions.IgnoreCase);
                        var match = regex.Match(attributeValue);

                        if (match.Groups.Count > 1 && match.Success)
                        {
                            return match.Groups[1].Value;
                        }
                    }
                }
                else // if (imageTags.Count == 1)
                {
                    // Scenario 1: One XML tag
                    var xmlTag = imageTags[0].Trim();
                    //var xmlTag2 = imageTags[1].Trim();

                    var xmlTagElement = FindElementByTagName(item, xmlTag);
                    //var xmlTagElement2 = FindElementByTagName(parent, xmlTag2);

                    if (xmlTagElement != null)// && xmlTagElement2 != null)
                    {
                        return xmlTagElement.Value;// ?? xmlTagElement2.Value;
                    }
                }

                return string.Empty;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        private static string StripHtmlTags(string text)
        {
            return string.IsNullOrEmpty(text) ? string.Empty :
                Regex.Replace(text, "<.*?>", string.Empty);
        }
        private static string RemoveScriptTags(string input)
        {
            return string.IsNullOrEmpty(input) ? input :
                // Regex to match <script>...</script>
                Regex.Replace(input, "<script.*?>.*?</script>", string.Empty, RegexOptions.Singleline);
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

            return regexMetaCharacters.Any(pattern.Contains);
        }
        #endregion
    }

}
