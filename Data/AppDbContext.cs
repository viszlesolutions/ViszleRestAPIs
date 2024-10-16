using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ViszleRestAPI.Models;
namespace ViszleRestAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<AgentModel> Agents { get; set; }
        public DbSet<SiteDetailsModel> SiteDetails { get; set; }

        public DbSet<SiteImageModel> SiteImages { get; set; }

        public DbSet<AdminApprovalModel> AdminApprovals { get; set; }
    }
}
