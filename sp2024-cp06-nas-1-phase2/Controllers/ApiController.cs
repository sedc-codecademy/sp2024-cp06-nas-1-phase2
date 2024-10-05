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
        public async Task<IActionResult> GetAllNews()
        {
            try
            {
                var result = await _apiService.GetArticlesAsync();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
