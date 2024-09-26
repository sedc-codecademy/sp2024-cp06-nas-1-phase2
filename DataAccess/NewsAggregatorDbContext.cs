using DomainModels;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class NewsAggregatorDbContext : DbContext
    {
        public NewsAggregatorDbContext(DbContextOptions<NewsAggregatorDbContext> options) : base(options)
        {
            
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<RssSource> RssSources { get; set; }
        public DbSet<UrlToImageConfig> UrlToImageConfigs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RssSource>()
                .HasMany(rs => rs.Articles)        // RssSource can have many Articles
                .WithOne(a => a.RssSource)         // Each Article has one RssSource
                .HasForeignKey(a => a.RssSourceId) // Foreign key in Article
                .OnDelete(DeleteBehavior.Cascade); // Define delete behavior

            // Define one-to-many relationship between RssSource and UrlToImageConfigs
            builder.Entity<RssSource>()
                .HasMany(rs => rs.UrlToImageConfigs) // RssSource can have many UrlToImageConfigs
                .WithOne(uic => uic.RssSource)       // Each UrlToImageConfig belongs to one RssSource
                .HasForeignKey(uic => uic.RssSourceId) // Foreign key in UrlToImageConfig
                .OnDelete(DeleteBehavior.Cascade);

            // Optionally, you can define indexes for performance on frequent queries
            builder.Entity<Article>()
                .HasIndex(a => a.FeedUrl) // Index on FeedUrl for faster lookups
                .IsUnique(false);         // Allow multiple articles from the same feed

            // Seed data using the PopulateDb class
            PopulateDb.Seed(builder);
        }
    }
}
