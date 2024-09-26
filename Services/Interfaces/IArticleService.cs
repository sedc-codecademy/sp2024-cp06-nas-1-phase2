using DomainModels;
using System.Xml.Linq;

namespace Services.Interfaces
{
    public interface IArticleService
    {
        public List<Article> CreateArticles(List<XElement> items, RssSource urlConfig);
    }
}
