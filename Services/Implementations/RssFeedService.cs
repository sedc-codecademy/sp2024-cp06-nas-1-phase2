using System.Text.RegularExpressions;
using System.Xml.Linq;
using Services.Interfaces;

namespace Services.Implementations
{
    public class RssFeedService : IRssFeedService
    {
        private readonly HttpClient _httpClient;

        public RssFeedService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        /*
        public async Task<string> FetchRssFeedXmlAsync(string feedUrl)
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "RSSFetcher/1.0");
            var response = await _httpClient.GetAsync(feedUrl);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public List<XElement> ParseRssItems(string xmlData)
        {
            xmlData = RemoveScriptTags(xmlData);
            var xmlDoc = XDocument.Parse(xmlData);
            return xmlDoc.Descendants("item").ToList();
        }
        private static string RemoveScriptTags(string xmlData)
        {
            return Regex.Replace(xmlData, "<script.*?</script>", "", RegexOptions.IgnoreCase | RegexOptions.Singleline);
        }
        */
    }

}
