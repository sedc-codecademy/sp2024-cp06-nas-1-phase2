using DataAccess;
using Microsoft.EntityFrameworkCore;
using Shared.Modules;

namespace UpdateArticleDates
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<NewsAggregatorDbContext>()
                .UseSqlServer("Server=Server;Database=NewsAggregatorDb;User Id=dnisko;Password=1q2w3e4r5t;") // Use your actual connection string
                .Options;

            using (var context = new NewsAggregatorDbContext(options))
            {
                var articles = await context.Articles.ToListAsync();

                foreach (var article in articles)
                {
                    var parsedDate = DateParser.ParseDate(article.PubDate);
                    if (parsedDate.HasValue)
                    {
                        // Assign the parsed DateTime directly to the PubDate property
                        article.PubDate = parsedDate.Value.ToString(); 
                    }
                }

                await context.SaveChangesAsync();
                Console.WriteLine("Publication dates updated successfully.");
            } 
        }
    }
}
