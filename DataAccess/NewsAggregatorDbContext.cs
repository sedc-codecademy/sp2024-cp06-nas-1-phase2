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
        public DbSet<UrlToImageConfig> UrlToImageConfig { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            PopulateDb.Seed(builder);
        }
    }
}
