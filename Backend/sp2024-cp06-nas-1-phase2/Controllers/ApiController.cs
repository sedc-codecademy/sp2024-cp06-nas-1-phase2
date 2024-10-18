using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace sp2024_cp06_nas_1_phase2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IApiService _apiService;

        public ApiController(IApiService apiService)
        {
            _apiService = apiService;
        }
        [HttpGet("getNews")]
        public async Task<IActionResult> GetAllNews(int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                var paginatedResult = await _apiService.GetArticlesAsync(pageNumber, pageSize);
    
                return Ok(paginatedResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getNewsBySource/{rssFeedId}")]
        public async Task<IActionResult> GetNewsBySource(int rssFeedId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var result = await _apiService.GetPagedArticlesBySourceAsync(rssFeedId, pageNumber, pageSize);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
