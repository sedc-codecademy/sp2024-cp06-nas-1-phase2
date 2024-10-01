using System.Text.RegularExpressions;
using System.Xml.Linq;
using DomainModels;
using Services.Interfaces;

namespace Services.Implementations
{
    public class ArticleService : IArticleService
    {
        public List<Article> CreateArticles(List<XElement> items, RssSource rssSource)
        {
            var articles = new List<Article>();
            foreach (var item in items)
            {
                var article = new Article
                {
                    RssSourceId = rssSource.Id,
                    FeedUrl = rssSource.FeedUrl,
                    Title = GetElementValue(item, rssSource.Title),
                    Description = StripHtmlTags(GetElementValue(item, rssSource.Description)),
                    Link = GetElementValue(item, rssSource.Link),
                    Author = GetElementValue(item, rssSource.Author),
                    PubDate = GetElementValue(item, rssSource.PubDate),
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
    }
}
