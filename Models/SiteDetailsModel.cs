using System.ComponentModel.DataAnnotations;

namespace ViszleRestAPI.Models
{
    public class SiteDetailsModel 
    {
        [Key]
        public int SiteId { get; set; }
        public int AgentId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; } = "Pending";
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property for foreign key relationship
        //public AgentModel Agent { get; set; }
    }
}
