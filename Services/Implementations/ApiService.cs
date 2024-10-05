using Services.Interfaces;
using DTOs.Article;

namespace Services.Implementations
{
    public class ApiService : IApiService
    {
        private readonly IArticleService _articleService;

        public ApiService(IArticleService articleService)
        {
            _articleService = articleService;
        }
        public async Task<IEnumerable<ArticleDto>> GetArticlesAsync()
        {
            return await _articleService.GetAllArticlesAsync();
        }

        public async Task<IEnumerable<ArticleDto>> GetArticlesBySourceAsync(int rssFeedId)
        {
            return await _articleService.GetAllArticlesBySourceAsync(rssFeedId);
        }
        //public async Task ProcessFeedsAsync()
        //{
        //    // Step 1: Get all feeds
        //    var feeds = await _rssFeedService.GetAllRssFeedsAsync();

        //    foreach (var feed in feeds)
        //    {
        //        // Step 2: Parse RSS feed (this logic would be specific to your app)
        //        var articles = await _articleService.GetAllArticlesBySourceAsync(feed.Id);

        //        // Step 3: Save parsed articles
        //        await _articleService.AddArticlesAsync(articles);
        //    }
        //}
        //public async Task FetchRssFeedsAsync()//<List<ArticleDto>> FetchRssFeedsAsync()
        //{
        //    var rssFeeds = await _rssFeedService.GetAllRssFeedsAsync();

        //    foreach (var rssFeed in rssFeeds)
        //    {
        //        var xmlData = await _rssFeedService.FetchRssFeedXmlAsync(rssFeed.FeedUrl);//_rssFeedService.FetchRssFeedXmlAsync(rssFeed.FeedUrl);
        //        var parsedArticles = ParseRssItems(xmlData);//, rssFeed);
        //        //await _articleService.AddArticlesAsync(parsedArticles);

        //        var articleDtos = new List<ArticleDto>();

        //        foreach (var item in parsedArticles)
        //        {
        //            var articleDto = new ArticleDto
        //            {
        //                Title = GetElementValue(item, rssFeed.Title),
        //                Description = GetElementValue(item, rssFeed.Description),
        //                Link = GetElementValue(item, rssFeed.Link),
        //                Author = GetElementValue(item, rssFeed.Author),
        //                PubDate = GetElementValue(item, rssFeed.PubDate),
        //                FeedUrl = rssFeed.FeedUrl,
        //                RssFeedId = rssFeed.Id,
        //                UrlToImage = await _rssFeedService.ExtractImageUrl(item, rssFeed)
        //            };

        //            articleDtos.Add(articleDto);
        //        }
        //        await _articleService.AddArticlesAsync(articleDtos);
        //    }
        //    //var allArticles = new List<Article>();

        //    //// Retrieve RSS feeds from the database
        //    //var rssFeeds = await _rssFeedRepository.GetAllAsync();

        //    //foreach (var rssFeed in rssFeeds)
        //    //{
        //    //    try
        //    //    {
        //    //        var xmlData = await FetchRssFeedXmlAsync(rssFeed.FeedUrl);
        //    //        var parsedArticles = ParseRss(xmlData, rssFeed);

        //    //        allArticles.AddRange(parsedArticles);

        //    //        foreach (var article in parsedArticles)
        //    //        {
        //    //            await _articleRepository.AddAsync(article);
        //    //        }
        //    //    }
        //    //    catch (Exception ex)
        //    //    {
        //    //        Console.WriteLine($"Error fetching or parsing RSS feed from {rssFeed.Source}: {ex.Message}");
        //    //    }
        //    //}

        //    //var allArticlesDto = allArticles.Select(x => _mapper.Map<ArticleDto>(x)).ToList();
        //    //return allArticlesDto;
        //}
        ///*
        //private void ExtractImagesForArticles(List<Article> articles, List<XElement> items,
        //    ICollection<UrlToImageConfig> imageConfigs)
        //{
        //    for (int i = 0; i < articles.Count; i++)
        //    {
        //        foreach (var config in imageConfigs)
        //        {
        //            var imageElement = items[i].Descendants(config.Query).FirstOrDefault();
        //            if (imageElement != null)
        //            {
        //                if (!string.IsNullOrEmpty(config.Attribute))
        //                {
        //                    articles[i].UrlToImage = imageElement.Attribute(config.Attribute)?.Value ?? string.Empty;
        //                }
        //                else if (!string.IsNullOrEmpty(config.Regex))
        //                {
        //                    var regex = new Regex(config.Regex);
        //                    var match = regex.Match(imageElement.Value);
        //                    articles[i].UrlToImage = match.Success ? match.Value : string.Empty;
        //                }
        //            }
        //        }
        //    }
        //}
        //*/
        //private async Task<string> FetchRssFeedXmlAsync(string feedUrl)
        //{
        //    var response = await _httpClient.GetAsync(feedUrl);
        //    response.EnsureSuccessStatusCode();
        //    return await response.Content.ReadAsStringAsync();
        //}
        ///*
        //private static async Task<string> FetchRssFeedXmlAsync(string feedUrl)
        //{
        //    using (var httpClient = new HttpClient())
        //    {
        //        // Set the User-Agent header to your desired value
        //        httpClient.DefaultRequestHeaders.Add("User-Agent", "RSSFetcher/1.0");

        //        var response = await httpClient.GetAsync(feedUrl);
        //        response.EnsureSuccessStatusCode();

        //        return await response.Content.ReadAsStringAsync();
        //    }
        //}
        //*/
        //private static List<XElement> ParseRssItems(string xmlData)//, RssFeedDto rssFeed)
        //{
        //    var xmlDoc = XDocument.Parse(xmlData);
        //    var items = xmlDoc.Descendants("item").ToList();
        //    return items;
        //    /*
        //    var articles = new List<Article>();
        //    var xmlDoc = XDocument.Parse((xmlData));
        //    var items = xmlDoc.Descendants("item");
        //    try
        //    {
        //        xmlData = RemoveScriptTags(xmlData);
        //        //var xmlDoc = XDocument.Parse(xmlData);

        //        foreach (var item in items)// xmlDoc.Descendants("item"))
        //        {
        //            var article = new Article
        //            {
        //                RssFeedId = rssFeed.Id,
        //                Title = GetElementValue(item, rssFeed.Title),
        //                Description = StripHtmlTags(GetElementValue(item, rssFeed.Description)),
        //                Link = GetElementValue(item, rssFeed.Link),
        //                Author = GetElementValue(item, rssFeed.Author),
        //                PubDate = GetElementValue(item, rssFeed.PubDate),
        //                FeedUrl = rssFeed.FeedUrl

        //                //Source = rssFeed.Source,
        //                //SourceUrl = rssFeed.SourceUrl,
        //                //FeedUrl = rssFeed.FeedUrl,
        //                //Title = GetElementValue(item, rssFeed.Title),
        //                //Description = StripHtmlTags(GetElementValue(item, rssFeed.Description)),
        //                //Link = GetElementValue(item, rssFeed.Link),
        //                //Author = GetElementValue(item, rssFeed.Author),
        //                //PubDate = GetElementValue(item, rssFeed.PubDate),
        //            };
        //            var urlToImageConfigs = _urlToImageConfigService.GetUrlToImageConfigsAsync(rssFeed.Id).Result;
        //            article.UrlToImage = ExtractImageUrl(item, urlToImageConfigs);
        //            articles.Add(article);
        //        }
        //}
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Error parsing RSS feed: {ex.Message}");
        //    }

        //    return articles;*/
        //}
        //private static string ExtractImageUrl(XElement item, IEnumerable<UrlToImageConfigDto> urlToImageConfigs)
        //{
        //    foreach (var config in urlToImageConfigs)
        //    {
        //        var element = item.Descendants(config.Query).FirstOrDefault();
        //        if (element != null)
        //        {
        //            if (!string.IsNullOrEmpty(config.Attribute))
        //            {
        //                return element.Attribute(config.Attribute)?.Value ?? string.Empty;
        //            }
        //            if (!string.IsNullOrEmpty(config.Regex))
        //            {
        //                var match = Regex.Match(element.Value, config.Regex);
        //                if (match.Success)
        //                {
        //                    return match.Value;
        //                }
        //            }
        //        }
        //    }
        //    return string.Empty;
        //}
        ///*
        //private static XElement FindElementByTagName(XElement parent, string tagName)
        //{
        //    if (tagName.Contains(":"))
        //    {
        //        var parts = tagName.Split(':');
        //        var prefix = parts[0];
        //        var localName = parts[1];
        //        var ns = parent.GetNamespaceOfPrefix(prefix);
        //        return parent.Descendants(ns + localName).FirstOrDefault();
        //    }
        //    else
        //    {
        //        return parent.Descendants(tagName).FirstOrDefault();
        //    }
        //}
        //*/
        //private static string GetElementValue(XElement parent, string tagName)
        //{
        //    if (string.IsNullOrEmpty(tagName)) return string.Empty;
        //    XElement element = parent.Descendants(tagName).FirstOrDefault();
        //    return element?.Value.Trim() ?? string.Empty;
        //    //if (tagName == "")
        //    //{
        //    //    return "";
        //    //}

        //    //XElement element = FindElementByTagName(parent, tagName);
        //    //return element?.Value.Trim() ?? string.Empty;
        //}
        //private static string StripHtmlTags(string text, List<string> allowedTags = null)
        //{
        //    if (allowedTags == null || allowedTags.Count == 0)
        //    {
        //        return Regex.Replace(text, "<.*?>", string.Empty);
        //    }

        //    var tagList = string.Join("|", allowedTags);
        //    var regex = new Regex($"<(?!/?(?:{tagList})\\b)[^>]*>", RegexOptions.IgnoreCase);
        //    return regex.Replace(text, string.Empty);
        //}
        //private static string RemoveScriptTags(string xmlData)
        //{
        //    return Regex.Replace(xmlData, "<script.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        //}
    }
}
