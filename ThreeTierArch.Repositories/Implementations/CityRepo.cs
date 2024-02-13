using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreeTierArch.Entities;
using ThreeTierArch.Repositories.Interfaces;

namespace ThreeTierArch.Repositories.Implementations
{
    public class CityRepo : ICityRepo
    {
        private readonly ApplicationDbContext _context;
        public CityRepo(ApplicationDbContext dbContext) {
            _context = dbContext;
        }

        public async Task DeleteCity(City city)
        {
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<City>> GetAll()
        {
            return await _context.Cities.Include(m => m.State).ThenInclude(y => y.Country).ToListAsync();
        }

        public async Task<City?> GetByCityId(int cityId)
        {
            return await _context.Cities.Where( c=> c.Id == cityId)
                .Include(m => m.State).ThenInclude(y => y.Country).FirstOrDefaultAsync();
        }

        public async Task SaveCity(City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCity(City city)
        {
            _context.Cities.Update(city);
            await _context.SaveChangesAsync();
        }
    }
}
