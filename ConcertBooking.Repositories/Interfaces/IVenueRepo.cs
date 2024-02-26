using ConcertBooking.Entities;

namespace ConcertBooking.Repositories.Interfaces
{
    public interface IVenueRepo
    {
        public Task<IEnumerable<Venue>> GetAll();
        public Task<Venue> GetById(int id);
        public Task Save(Venue venue);
        public Task Edit(Venue venue);
        public Task RemoveData(Venue venue);
    }
}
