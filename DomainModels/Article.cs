namespace DomainModels
{
    public class Article : BaseClass
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Author { get; set; }
        public string PubDate { get; set; }
        public string FeedUrl { get; set; }

        // Foreign key to RssSource
        public int RssSourceId { get; set; }
        public RssSource RssSource { get; set; } // Navigation property to RssSource

        public string UrlToImage { get; set; } // For image extraction
    }
}
