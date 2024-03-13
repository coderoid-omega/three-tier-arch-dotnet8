using ConcertBooking.Entities;

namespace ConcertBooking.Repositories.Interfaces
{
    public interface IBookingRepo
    {
        public Task AddBooking(Booking booking);
        public Task<IEnumerable<Booking>> GetAll(int concertId);
    }
}
