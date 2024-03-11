using Microsoft.AspNetCore.Identity;

namespace ConcertBooking.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
    }
}
