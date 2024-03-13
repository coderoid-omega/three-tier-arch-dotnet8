using ConcertBooking.Entities;
using ConcertBooking.Repositories.Interfaces;

namespace ConcertBooking.Repositories.Implementations
{
    public class BookingRepo : IBookingRepo
    {
        private readonly ApplicationDbContext _context;
        public BookingRepo(ApplicationDbContext context) {  _context = context; }
        public async Task AddBooking(Booking booking)
        {
            await _context.Bookings.AddAsync(booking);
            await _context.SaveChangesAsync();
        }
    }
}
