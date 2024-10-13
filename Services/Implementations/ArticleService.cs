using AutoMapper;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.Article;
using Services.Helpers;
using Services.Interfaces;

namespace Services.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerHelper _logger;

        public ArticleService(IArticleRepository articleRepository,
            IMapper mapper,
            ILoggerHelper logger)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<PaginatedResult<ArticleDto>> GetPagedArticlesAsync(int pageNumber, int pageSize)
        {
            var totalCount = await _articleRepository.GetTotalCountAsync(); // Method to get total articles count
            var articles = await _articleRepository.GetPaginatedAsync(pageNumber, pageSize);

            var articleDtos = articles.Select(article => _mapper.Map<ArticleDto>(article));

            return new PaginatedResult<ArticleDto>(articleDtos, totalCount, pageNumber, pageSize);
        }

        public async Task<IEnumerable<ArticleDto>> GetAllArticlesBySourceAsync(int rssFeedId)
        {
            try
            {
                //var articles = await _articleRepository.GetAllAsync();
                //var filteredArticles = articles.Where(x => x.RssFeedId == rssFeedId);
                //return _mapper.Map<IEnumerable<ArticleDto>>(filteredArticles);
                var articles = await _articleRepository.GetArticlesByRssSourceIdAsync(rssFeedId);
                return _mapper.Map<IEnumerable<ArticleDto>>(articles);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching articles by source.", ex);
            }
        }
        public async Task AddArticlesAsync(IEnumerable<ArticleDto> addArticles, CancellationToken cancellationToken)
        {
            try
            {
                var articles = _mapper.Map<IEnumerable<Article>>(addArticles);
                var newArticles = new List<Article>();
                
                foreach (var article in articles)
                {
                    var latestArticle = await _articleRepository.GetLatestArticleByRssFeedIdAsync(article.RssFeedId);

                    if (latestArticle != null && latestArticle.Title == article.Title &&
                        latestArticle.Link == article.Link)
                    {
                        break;
                    }
                    newArticles.Add(article);
                }

                if (newArticles.Any())
                {
                    newArticles.Reverse();
                    await _articleRepository.AddRangeAsync(newArticles, cancellationToken);
                    _logger.LogInfo($"Added {newArticles.Count} articles from " +
                                    $"{newArticles.Select(x => x.RssFeed.Source)}");
                }
                else
                {
                    _logger.LogInfo($"No new articles from" +
                                    $"{newArticles.Select(x => x.RssFeed.Source)}");

                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Cannot add articles.");
                throw new Exception("Error adding articles.", ex);
            }
        }
        public async Task<IEnumerable<ArticleDto>> GetPagedArticlesBySourceAsync(int rssFeedId, int pageNumber, int pageSize)
        {
            var articles = await _articleRepository.GetPagedArticlesByRssSourceIdAsync(rssFeedId, pageNumber, pageSize);

            // Convert to DTOs if necessary
            return articles.Select(a => new ArticleDto
            {
                // Map properties from Article to ArticleDto
                Id = a.Id,
                Title = a.Title,
                Link = a.Link,
                // Add other properties as needed
            });
        }
    }
}
