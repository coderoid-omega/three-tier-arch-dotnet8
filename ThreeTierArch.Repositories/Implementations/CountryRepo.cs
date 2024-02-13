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
    public class CountryRepo : ICountryRepo
    {
        private readonly ApplicationDbContext _context;
        public CountryRepo(ApplicationDbContext dbContext) {
            _context = dbContext;
        }
        public async Task DeleteCountry(Country country)
        {
            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
        }

        public async  Task<IEnumerable<Country>> GetAllCountry()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<Country?> GetCountryById(int id)
        {
            return await _context.Countries.FindAsync(id);
        }

        public async Task SaveCountry(Country country)
        {
            _context.Countries.Add(country);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCountry(Country country)
        {
            _context.Countries.Update(country);
            await _context.SaveChangesAsync();
        }
    }
}
