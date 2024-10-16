using System;
using System.ComponentModel.DataAnnotations;
namespace ViszleRestAPI.Models
{
    public class AgentModel
    {
        [Key]
        public int AgentId { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string PasswordHash { get; set; }
        public bool IsPhoneConfirmed { get; set; } = false;
        public bool IsAdmin { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
