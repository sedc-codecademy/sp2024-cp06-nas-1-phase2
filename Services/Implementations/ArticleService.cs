using System.Text.RegularExpressions;
using System.Xml.Linq;
using DomainModels;
using Services.Interfaces;

namespace Services.Implementations
{
    public class ArticleService : IArticleService
    {
        public List<Article> CreateArticles(List<XElement> items, RssSource urlConfig)
        {
            var articles = new List<Article>();
            foreach (var item in items)
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
