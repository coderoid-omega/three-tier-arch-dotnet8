using ConcertBooking.Entities;
using ConcertBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConcertBooking.Repositories.Implementations
{
    public class TicketRepo : ITicketRepo
    {
        private readonly ApplicationDbContext _context;
        public TicketRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<int>> GetBookedSeatNumber(int concertId)
        {
            /*var list = await _context.Tickets.FromSqlRaw<Ticket>("Select t.* from Tickets as t inner Join Bookings b on b.Id = t.BookingId where t.isbooked = 1 and b.concertid =" + concertId).ToListAsync();
            var bookedSeats = list.Select(m => m.SeatNumber).ToList();
            */
            var bookedSeats = await _context.Tickets.Include(m => m.Booking).Where(t => t.Booking.ConcertId == concertId)
                .Select(s => s.SeatNumber)
                .ToListAsync();
            return bookedSeats;
        }

        public async Task<IEnumerable<Booking>> GetUserBookings(string userId)
        {
            var bookings = await _context.Bookings
                .Where(m => m.UserId == userId)
                .Include(m => m.Tickets)
                .Include(m => m.Concert)
                .ToListAsync();
            return bookings;
        }
    }
}
