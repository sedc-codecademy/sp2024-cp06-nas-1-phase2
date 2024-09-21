using DomainModels;

namespace DTOs
{
    public class RssSourceDto
    {
        public string Source { get; set; }
        public string SourceUrl { get; set; }
        public string FeedUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Author { get; set; }
        public string PubDate { get; set; }
        public List<string> UrlToImage { get; set; }
        //public UrlToImageConfig UrlToImageConfig { get; set; }
    }
}
