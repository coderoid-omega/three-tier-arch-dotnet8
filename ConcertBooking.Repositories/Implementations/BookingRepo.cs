using ConcertBooking.Entities;
using ConcertBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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

        public async Task<IEnumerable<Booking>> GetAll(int concertId)
        {
            var bookings= await _context.Bookings.Where(m => m.ConcertId == concertId)
                .Include(m => m.Tickets)
                .Include(m => m.Concert)
                .Include(m => m.User)
                .ToListAsync();
            return bookings;
        }
    }
}
