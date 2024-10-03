using System.Text.RegularExpressions;
using System.Xml.Linq;
using AutoMapper;
using DataAccess.Implementations;
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

        public ArticleService(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
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
                var articles = await _articleRepository.GetAllAsync();
                var filteredArticles = articles.Where(x => x.RssFeedId == rssFeedId);
                return _mapper.Map<IEnumerable<ArticleDto>>(filteredArticles);
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
                await _articleRepository.AddRangeAsync(articles);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding articles.", ex);
            }
        }
        
    }
}
