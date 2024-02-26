using ConcertBooking.Entities;
using ConcertBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ConcertBooking.Repositories.Implementations
{
    public class VenueRepo : IVenueRepo
    {
        private readonly ApplicationDbContext _context;
        public VenueRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Edit(Venue venue)
        {
            _context.Venues.Update(venue);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Venue>> GetAll()
        {
            return await _context.Venues.ToListAsync();
        }

        public async Task<Venue> GetById(int id)
        {
            return await _context.Venues.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task RemoveData(Venue venue)
        {
             _context.Venues.Remove(venue);
            await _context.SaveChangesAsync();
        }

        public async Task Save(Venue venue)
        {
            await _context.Venues.AddAsync(venue);
            await _context.SaveChangesAsync();
        }
    }
}
