using Microsoft.AspNetCore.Identity;

namespace ConcertBooking.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber {  get; set; }
        public string? PinCode { get; set; }
    }
}
