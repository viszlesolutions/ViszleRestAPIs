using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViszleRestAPI.Models;
using ViszleRestAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ViszleRestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsRegisterController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AgentsRegisterController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/Agents
        [HttpPost]
        public async Task<ActionResult<AgentModel>> RegisterAgent([FromBody] AgentModel agent)
        {
            // Check if the phone number already exists
            var existingAgent = await _context.Agents.FirstOrDefaultAsync(a => a.PhoneNumber == agent.PhoneNumber);
            if (existingAgent != null)
            {
                return BadRequest("Phone number is already registered.");
            }

            // Hash the password before saving (you'd use a library like BCrypt or ASP.NET Identity for proper hashing)
            agent.PasswordHash = BCrypt.Net.BCrypt.HashPassword(agent.PasswordHash);

            _context.Agents.Add(agent);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(RegisterAgent), new { id = agent.AgentId }, agent);
        }
    }
}
