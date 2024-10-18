using System.ComponentModel.DataAnnotations;

namespace DomainModels
{
    public class RssFeed : BaseClass
    {
        [Required]
        public string Source { get; set; }
        //[Required]
        public string SourceUrl { get; set; }
        //[Required]
        public string FeedUrl { get; set; }
        public string Title { get; set; }
        //[Required]
        public string Description { get; set; }
        public string Link { get; set; }
        public string Author { get; set; }
        public string PubDate { get; set; }
        //[Required]
        public string Query { get; set; }
        public string? Attribute { get; set; }
        public string? Regex { get; set; }

        // Navigation properties
        public ICollection<Article> Articles { get; set; } // RssFeed has many Articles
    }
}
