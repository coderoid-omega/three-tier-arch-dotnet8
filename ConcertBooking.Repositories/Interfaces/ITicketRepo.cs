using ConcertBooking.Entities;

namespace ConcertBooking.Repositories.Interfaces
{
    public interface ITicketRepo
    {
        public Task<List<int>> GetBookedSeatNumber(int concertId);
        public Task<IEnumerable<Booking>> GetUserBookings(string userId);
    }
}
