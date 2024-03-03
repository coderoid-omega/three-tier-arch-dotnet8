using ConcertBooking.Entities;
using ConcertBooking.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;

namespace ConcertBooking.Repositories.Implementations
{
    public class ConcertRepo : IConcertRepo
    {
        private readonly ApplicationDbContext _context;
        public ConcertRepo(ApplicationDbContext dbContext) 
        {
            _context = dbContext;
        }
        public async Task Edit(Concert artist)
        {
            _context.Concerts.Update(artist);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Concert>> GetAll()
        {
            return await _context.Concerts.Include(a => a.Artist).Include(v => v.Venue).ToListAsync();
        }

        public async Task<Concert> GetById(int id)
        {
            return await _context.Concerts.Include(m => m.Artist).Include(n => n.Venue).FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task RemoveData(Concert concert)
        {
            _context.Concerts.Remove(concert);
            await _context.SaveChangesAsync();
        }

        public async Task Save(Concert concert)
        {
            await _context.Concerts.AddAsync(concert);
            await _context.SaveChangesAsync();
        }
    }
}
