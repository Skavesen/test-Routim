using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SearchApiController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;

        public SearchApiController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("find")]
        public async Task<IActionResult> Find([FromBody] string searchQuery)
        {
            var existingResult = await _dbContext.SearchResults.FirstOrDefaultAsync(r => r.SearchQuery == searchQuery);

            if (existingResult == null)
            {
            }

            return Ok(existingResult);
        }

        [HttpGet("find")]
        public async Task<IActionResult> GetAllResults()
        {
            var results = await _dbContext.SearchResults.ToListAsync();
            return Ok(results);
        }

        [HttpDelete("find/{id}")]
        public async Task<IActionResult> DeleteResult(int id)
        {
            var result = await _dbContext.SearchResults.FirstOrDefaultAsync(r => r.Id == id);
            if (result != null)
            {
                _dbContext.SearchResults.Remove(result);
                await _dbContext.SaveChangesAsync();
            }
            return NoContent();
        }
    }
}
