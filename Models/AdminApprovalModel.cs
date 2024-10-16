using System;
using System.ComponentModel.DataAnnotations;
namespace ViszleRestAPI.Models
{
    public class AdminApprovalModel
    {
        [Key]
        public int ApprovalId { get; set; }
        public int SiteId { get; set; }
        public int AdminId { get; set; }
        public string Status { get; set; } // Approved or Rejected
        public string Comment { get; set; }
        public DateTime ApprovedAt { get; set; } = DateTime.Now;

        // Navigation properties for foreign key relationships
        //public SiteDetailsModel SiteDetail { get; set; }
        //public AgentModel Admin { get; set; }
    }
}
