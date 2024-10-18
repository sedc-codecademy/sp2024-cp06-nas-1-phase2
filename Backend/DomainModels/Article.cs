namespace DomainModels
{
    public class Article : BaseClass
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string? Author { get; set; }
        public string? PubDate { get; set; }
        public string FeedUrl { get; set; }

        // Foreign key to RssFeed
        public int RssFeedId { get; set; }
        public RssFeed RssFeed { get; set; } // Navigation property to RssFeed

        public string UrlToImage { get; set; } // For image extraction
    }
}
