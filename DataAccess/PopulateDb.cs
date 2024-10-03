using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public static class PopulateDb
    {
        public static void Seed(ModelBuilder builder)
        {
            builder.Entity<RssFeed>()
                .HasData(new List<RssFeed>
                {
                    new RssFeed
                    {
                        Id = 1,
                        Source = "MIA",
                        SourceUrl = "https://mia.mk",
                        FeedUrl = "https://mia.mk/feed",
                        Title = "title",
                        Description = "description",
                        Link = "link",
                        Author = "author",
                        PubDate = "pubDate"
                    },
                    new RssFeed
                    {
                        Id = 2,
                        Source = "Telma",
                        SourceUrl = "https://telma.com.mk",
                        FeedUrl = "https://telma.com.mk/feed/",
                        Title = "title",
                        Description = "content:encoded",
                        Link = "link",
                        Author = "dc:creator",
                        PubDate = "pubDate"
                    },
                    new RssFeed
                    {
                        Id = 3,
                        Source = "24Vesti",
                        SourceUrl = "https://24.mk",
                        FeedUrl = "https://admin.24.mk/api/rss.xml",
                        Title = "title",
                        Description = "content",
                        Link = "link",
                        Author = "",
                        PubDate = "pubDate"
                    },
                    new RssFeed
                    {
                        Id = 4,
                        Source = "Sitel",
                        SourceUrl = "https://sitel.com.mk",
                        FeedUrl = "https://sitel.com.mk/rss.xml",
                        Title = "title",
                        Description = "description",
                        Link = "link",
                        Author = "dc:creator",
                        PubDate = "pubDate"
                    },
                    new RssFeed
                    {
                        Id = 5,
                        Source = "Kanal5",
                        SourceUrl = "https://kanal5.com.mk",
                        FeedUrl = "https://kanal5.com.mk/rss.aspx",
                        Title = "title",
                        Description = "content",
                        Link = "link",
                        Author = "author",
                        PubDate = "pubDate"
                    }
                });

            builder.Entity<UrlToImageConfig>()
                .HasData(new List<UrlToImageConfig>
                {
                    new UrlToImageConfig
                    {
                        Id = 1,
                        RssSourceId = 1,
                        Query = "enclosure",
                        Attribute = "url"
                    },
                    new UrlToImageConfig
                    {
                        Id = 2,
                        RssSourceId = 2,
                        Query = "content:encoded",
                        Regex = @"<img[^>]*src=\""([^\""]*)\"""
                    },
                    new UrlToImageConfig
                    {
                        Id = 3,
                        RssSourceId = 3,
                        Query = "img",
                        Attribute = "src"
                    },
                    new UrlToImageConfig
                    {
                        Id = 4,
                        RssSourceId = 4,
                        Query = "description",
                        Regex = @"<img[^>]*src=\""([^\""]*)\"""
                    },
                    new UrlToImageConfig
                    {
                        Id = 5,
                        RssSourceId = 5,
                        Query = "thumbnail"
                    }
                });
        }
    }
}
