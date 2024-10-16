using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViszleRestAPI.Models;
using ViszleRestAPI.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace ViszleRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SiteImagesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SiteImagesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SiteImages
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SiteImageModel>>> GetSiteImages()
        {
            return await _context.SiteImages.ToListAsync();
        }

        // GET: api/SiteImages/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SiteImageModel>> GetSiteImage(int id)
        {
            var siteImage = await _context.SiteImages.FindAsync(id);

            if (siteImage == null)
            {
                return NotFound();
            }

            return siteImage;
        }

        // POST: api/SiteImages
        [HttpPost]
        public async Task<ActionResult<SiteImageModel>> CreateSiteImage([FromBody] SiteImageModel siteImageDto)
        {
            // Ensure the SiteDetail exists before adding an image
            var siteDetail = await _context.SiteDetails.FindAsync(siteImageDto.SiteId);
            if (siteDetail == null)
            {
                return BadRequest("Site not found.");
            }

            // Map DTO to SiteImage model
            var siteImage = new SiteImageModel
            {
                SiteId = siteImageDto.SiteId,
                ImageUrl = siteImageDto.ImageUrl,
                CreatedAt = siteImageDto.CreatedAt
            };

            _context.SiteImages.Add(siteImage);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSiteImage), new { id = siteImage.ImageId }, siteImage);
        }

        // PUT: api/SiteImages/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSiteImage(int id, [FromBody] SiteImageModel siteImageDto)
        {
            var siteImage = await _context.SiteImages.FindAsync(id);
            if (siteImage == null)
            {
                return NotFound();
            }

            siteImage.ImageUrl = siteImageDto.ImageUrl;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteImageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/SiteImages/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSiteImage(int id)
        {
            var siteImage = await _context.SiteImages.FindAsync(id);
            if (siteImage == null)
            {
                return NotFound();
            }

            _context.SiteImages.Remove(siteImage);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SiteImageExists(int id)
        {
            return _context.SiteImages.Any(e => e.ImageId == id);
        }
    }
}
