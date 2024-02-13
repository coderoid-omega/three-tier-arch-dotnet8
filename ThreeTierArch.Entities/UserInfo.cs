
using System.ComponentModel.DataAnnotations;

namespace ThreeTierArch.Entities
{
    public class UserInfo
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        public string Username { get; set; } = "";
        public string Password { get; set; } = "";
    }
}
