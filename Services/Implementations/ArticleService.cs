using AutoMapper;
using DataAccess.Interfaces;
using DomainModels;
using DTOs.Article;
using Services.Interfaces;

namespace Services.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerHelper _logger;

        public ArticleService(IArticleRepository articleRepository, IMapper mapper, ILoggerHelper logger)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<ArticleDto>> GetAllArticlesAsync()
        {
            try
            {
                var articles = await _articleRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<ArticleDto>>(articles);
            }
            catch (Exception ex)
            {
                throw new Exception("Error fetching articles.", ex);
            }
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

        public async Task AddArticlesAsync(IEnumerable<ArticleDto> addArticles)
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
                        break; // Stop comparison once a match is found
                    }

                    newArticles.Add(article);
                }

                if (newArticles.Any())
                {
                    newArticles.Reverse();
                    await _articleRepository.AddRangeAsync(newArticles);
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding articles.", ex);
            }
        }

    }
}
