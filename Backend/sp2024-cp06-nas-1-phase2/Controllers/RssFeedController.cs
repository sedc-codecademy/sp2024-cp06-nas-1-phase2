using AutoMapper;
using DomainModels;
using DTOs.RssFeed;
using Mappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace sp2024_cp06_nas_1_phase2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RssFeedController : ControllerBase
    {
        private readonly IRssFeedService _rssFeedService;

        public RssFeedController(IRssFeedService rssFeedService)
        {
            _rssFeedService = rssFeedService;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var response = await _rssFeedService.GetAllRssFeedsAsync();
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [HttpGet("getBySource")]
        public async Task<IActionResult> GetBySource(string source)
        {
            try
            {
                var response = await _rssFeedService.GetRssFeedBySourceAsync(source);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("addRssFeed")]
        public async Task<IActionResult> AddRssFeed([FromBody] AddRssFeedDto addRssFeedDto)
        {
            try
            {
                await _rssFeedService.AddRssFeedAsync(addRssFeedDto);
                return Ok($"The RSS {addRssFeedDto.Source} was added to the database!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> FetchAndProcessRssFeedsAsync(CancellationToken cancellationToken)
        {
            try
            {
                await _rssFeedService.FetchAndProcessRssFeedsAsync(cancellationToken);
                return Ok($"The Articles were added to the database!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
