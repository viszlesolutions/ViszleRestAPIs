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
    public class AdminApprovalsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AdminApprovalsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/AdminApprovals
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminApprovalModel>>> GetAdminApprovals()
        {
            //return await _context.AdminApprovals.Include(a => a.).Include(a => a.Admin).ToListAsync();
            return await _context.AdminApprovals.ToListAsync();
        }

        // GET: api/AdminApprovals/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminApprovalModel>> GetAdminApproval(int id)
        {
            //var adminApproval = await _context.AdminApprovals
            //    .Include(a => a.SiteDetail)
            //    .Include(a => a.Admin)
            //    .FirstOrDefaultAsync(a => a.ApprovalId == id);
            var adminApproval = await _context.AdminApprovals.FindAsync(id);
            if (adminApproval == null)
            {
                return NotFound();
            }

            return adminApproval;


        }

        

        // POST: api/AdminApprovals
        [HttpPost]
        public async Task<ActionResult<AdminApprovalModel>> CreateAdminApproval([FromBody] AdminApprovalModel adminApprovalDto)
        {
            // Ensure the SiteDetail and Admin exist before adding an approval
            var siteDetail = await _context.SiteDetails.FindAsync(adminApprovalDto.SiteId);
            var admin = await _context.Agents.FindAsync(adminApprovalDto.AdminId);

            if (siteDetail == null)
            {
                return BadRequest("Site not found.");
            }
            if (admin == null)
            {
                return BadRequest("Admin not found.");
            }

            // Map DTO to AdminApproval model
            var adminApproval = new AdminApprovalModel
            {
                SiteId = adminApprovalDto.SiteId,
                AdminId = adminApprovalDto.AdminId,
                Status = adminApprovalDto.Status,
                Comment = adminApprovalDto.Comment,
                ApprovedAt = adminApprovalDto.ApprovedAt
            };

            _context.AdminApprovals.Add(adminApproval);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAdminApproval), new { id = adminApproval.ApprovalId }, adminApproval);
        }

        // PUT: api/AdminApprovals/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAdminApproval(int id, [FromBody] AdminApprovalModel adminApprovalDto)
        {
            var adminApproval = await _context.AdminApprovals.FindAsync(id);
            if (adminApproval == null)
            {
                return NotFound();
            }

            adminApproval.Status = adminApprovalDto.Status;
            adminApproval.Comment = adminApprovalDto.Comment;
            adminApproval.ApprovedAt = adminApprovalDto.ApprovedAt;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminApprovalExists(id))
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

        // DELETE: api/AdminApprovals/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminApproval(int id)
        {
            var adminApproval = await _context.AdminApprovals.FindAsync(id);
            if (adminApproval == null)
            {
                return NotFound();
            }

            _context.AdminApprovals.Remove(adminApproval);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminApprovalExists(int id)
        {
            return _context.AdminApprovals.Any(e => e.ApprovalId == id);
        }
    }
}
