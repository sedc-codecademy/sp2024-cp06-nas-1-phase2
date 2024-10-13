namespace DTOs.Article
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string Author { get; set; }
        public string PubDate { get; set; }
        public string FeedUrl { get; set; }
        public int RssFeedId { get; set; }
        public string UrlToImage { get; set; }
    }
}
