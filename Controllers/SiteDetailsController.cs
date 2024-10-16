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
    public class SiteDetailsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SiteDetailsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SiteDetails
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SiteDetailsModel>>> GetSiteDetails()
        {
            return await _context.SiteDetails.ToListAsync();
        }

        // GET: api/SiteDetails/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SiteDetailsModel>> GetSiteDetail(int id)
        {
            var siteDetail = await _context.SiteDetails.FindAsync(id);

            if (siteDetail == null)
            {
                return NotFound();
            }

            return siteDetail;
        }

        // POST: api/SiteDetails
        [HttpPost]
        public async Task<ActionResult<SiteDetailsModel>> CreateSiteDetail([FromBody] SiteDetailsModel siteDetail)
        {
            // Ensure the Agent exists before adding a site
            var agent = await _context.Agents.FindAsync(siteDetail.AgentId);
            if (agent == null)
            {
                return BadRequest("Agent not found.");
            }

            _context.SiteDetails.Add(siteDetail);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSiteDetail), new { id = siteDetail.SiteId }, siteDetail);
        }

        // PUT: api/SiteDetails/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSiteDetail(int id, [FromBody] SiteDetailsModel siteDetail)
        {
            if (id != siteDetail.SiteId)
            {
                return BadRequest();
            }

            _context.Entry(siteDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteDetailExists(id))
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

        // DELETE: api/SiteDetails/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSiteDetail(int id)
        {
            var siteDetail = await _context.SiteDetails.FindAsync(id);
            if (siteDetail == null)
            {
                return NotFound();
            }

            _context.SiteDetails.Remove(siteDetail);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SiteDetailExists(int id)
        {
            return _context.SiteDetails.Any(e => e.SiteId == id);
        }
    }
}
