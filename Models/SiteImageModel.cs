using System.ComponentModel.DataAnnotations;

namespace ViszleRestAPI.Models
{
    public class SiteImageModel
    {
        [Key]
        public int ImageId { get; set; }
        public int SiteId { get; set; }
        public string ImageUrl { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property for foreign key relationship
       // public SiteDetailsModel SiteDetail { get; set; }
    }
}
